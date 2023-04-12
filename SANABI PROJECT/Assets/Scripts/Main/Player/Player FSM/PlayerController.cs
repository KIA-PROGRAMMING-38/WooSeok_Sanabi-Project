using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    #region playerStates 
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerWireShootState WireShootState { get; private set; }
    public PlayerWireState WireGrappledState { get; private set; }
    public PlayerWireGrappledWalkState WireGrappledWalkState { get; private set; }
    public PlayerWireGrappledInAirState WireGrappledInAirState { get; private set; }
    public PlayerWireGrappledIdleState WireGrappledIdleState { get; private set; }
    #endregion

    public Rigidbody2D playerRigidBody { get; private set; }
    public Animator BodyAnimator { get; private set; }
    public Animator ArmAnimator { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerArmController ArmController { get; private set; }

    public GrabController GrabController;

    public Transform armTransform;

    public PlayerData playerData;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    public PlayerAfterImage spritePrefab;
    #region variables
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    private int RightDirection = 1; // to avoid magicNumber

    private WaitForSeconds DashCooltime;
    private bool CanDash = true;
    private Vector2 distanceVec;
    private Vector2 perpendicularVec;
    private Vector2 direction;

    public bool isDashing = false;
    private float dashTimeLeft;

    private ObjectPool<PlayerAfterImage> WireDashPool;

    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        playerData = GetComponentInParent<PlayerData>();
        ArmController = GameObject.FindGameObjectWithTag("Arm").GetComponent<PlayerArmController>();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        RunState = new PlayerRunState(this, StateMachine, playerData, "run");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        WireShootState = new PlayerWireShootState(this, StateMachine, playerData, "wireShoot");
        WireGrappledState = new PlayerWireState(this, StateMachine, playerData, "wireGrappled");
        WireGrappledWalkState = new PlayerWireGrappledWalkState(this, StateMachine, playerData, "wireGrappledWalk");
        WireGrappledInAirState = new PlayerWireGrappledInAirState(this, StateMachine, playerData, "wireGrappledInAir");
        WireGrappledIdleState = new PlayerWireGrappledIdleState(this, StateMachine, playerData, "wireGrappledIdle");
    }

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = RightDirection;
        //Animators = GetComponentsInChildren<Animator>();
        BodyAnimator = GetComponent<Animator>();
        ArmAnimator = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();
        Input = GetComponentInParent<PlayerInput>();
        DashCooltime = new WaitForSeconds(playerData.DashCoolDown);
        WireDashPool = new ObjectPool<PlayerAfterImage>(CreateWireDashSprite, OnGetSpriteFromPool, OnReturnSpriteToPool);
        StateMachine.Initialize(IdleState);
    }

    Vector2 LastVelocity;
    public Vector2 VelocityDif;
    private void Update()
    {
        CurrentVelocity = playerRigidBody.velocity;
        VelocityDif = CurrentVelocity - LastVelocity;
        StateMachine.CurrentState.LogicUpdate();
        LastVelocity = CurrentVelocity;
        Debug.Log(FacingDirection);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityAll(float velocityX, float velocityY)
    {
        workspace.Set(velocityX, velocityY);
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetWallJumpVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }


    public void PlayerWireDash()
    {
        if (CanDash)
        {
            CanDash = false;
            isDashing = true;
            dashTimeLeft = playerData.DashTime;
            Input.UseDashInput();
            SetDashVelocity(Input.MovementInput.x);
            StartCoroutine(CountDashCooltime());
        }
    }

    public void AfterImage()
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                dashTimeLeft -= Time.deltaTime;
                WireDashPool.GetFromPool();
            }
            else
            {
                isDashing = false;
            }
        }
    }

    public PlayerAfterImage CreateWireDashSprite()
    {
        PlayerAfterImage sprite = Instantiate(spritePrefab);
        sprite.WireDashPool = WireDashPool;
        return sprite;
    }

    public void PlayerWireDashStop()
    {
        StopCoroutine(CountDashCooltime());
        CanDash = true;
    }

    private IEnumerator CountDashCooltime()
    {
        yield return DashCooltime;
        CanDash = true;
    }

    public void SetDashVelocity(float xInput)
    {
        if (transform.position.y < GrabController.AnchorPosition.y)
        {
            distanceVec = (GrabController.AnchorPosition - (Vector2)transform.position).normalized;
        }
        else
        {
            distanceVec = ((Vector2)transform.position - GrabController.AnchorPosition).normalized;
        }

        perpendicularVec = Vector2.Perpendicular(distanceVec);

        if (xInput == 0) // if no input
        {
            if (FacingDirection == RightDirection)
            {
                perpendicularVec *= -1;
            }
        }
        else // if input detected
        {
            if (xInput == 1) // rightKey
            {
                perpendicularVec *= -1;
            }
        }

        direction = (distanceVec + perpendicularVec).normalized;
        
        if (0 < CurrentVelocity.x * Input.MovementInput.x) // it's in the same direction
        {
            workspace.Set((direction.x * playerData.DashForce + CurrentVelocity.x), (direction.y * playerData.DashForce + CurrentVelocity.y));
        }
        else // opposite direction
        {
            workspace.Set(direction.x * playerData.DashForce, direction.y * playerData.DashForce);
        }
        
        playerRigidBody.velocity = workspace;
        CurrentVelocity = playerRigidBody.velocity;
    }

    public void AddXVelocityWhenGrappled(float xInput)
    {
        workspace.Set(playerData.grappleAddedForce * xInput, 0f);
        playerRigidBody.AddForce(workspace);
        CurrentVelocity = playerRigidBody.velocity;
    }

    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void CheckIfShouldFlipForMouseInput(float xDirection)
    {
        if (0f < xDirection) // if shoot right
        {
            if (FacingDirection == -RightDirection) // if looking left
            {
                Flip();
                //FlipForMouse();
            }
        }
        else // if shoot left
        {
            if (FacingDirection == RightDirection) // If looking right
            {
                Flip();
                //FlipForMouse();
            }
        }
    }


    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        // this will return true if anything conditioned above is detected, otherwise false
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
        // this will return true if anything conditioned above is detected, otherwise false
    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    private void Flip()
    {
        FacingDirection *= -1;
        Vector3 newScale = Vector3.one;
        newScale.x = FacingDirection;
        transform.localScale = newScale;
        armTransform.localScale = newScale;
        //transform.Rotate(0f, 180f, 0f);
        //armTransform.Rotate(0f, 180f, 0f);
    }


    private void OnGetSpriteFromPool(PlayerAfterImage sprite) => sprite.gameObject.SetActive(true);
    private void OnReturnSpriteToPool(PlayerAfterImage sprite) => sprite.gameObject.SetActive(false);

    #endregion
}
