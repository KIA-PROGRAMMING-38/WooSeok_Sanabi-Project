using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.XR;

public class SNBController : MonoBehaviour
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
    public PlayerWireGrappledState WireGrappledState { get; private set; }

    #endregion
    public Rigidbody2D playerRigidBody { get; private set; }

    private Animator[] Animators; // GetComponentInChildren 하면 자기꺼도 가져와서 걍 배열로 가져왔음
    public Animator BodyAnimator { get; private set; }
    public Animator ArmAnimator { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerWireController WireController { get; private set; }
    //public GrabController GrabController { get; private set; }
    public GrabController GrabController;

    public Transform armTransform;
    public DistanceJoint2D Joint { get; private set; }

    public PlayerData playerData;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    #region variables
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    private int RightDirection = 1; // to avoid magicNumber
    private WaitForSeconds DashCooltime;
    private bool CanDash = true;
    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        playerData = GetComponentInParent<PlayerData>();
        WireController = GetComponentInChildren<PlayerWireController>();
        Joint = GetComponent<DistanceJoint2D>();

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
        WireGrappledState = new PlayerWireGrappledState(this, StateMachine, playerData, "wireGrappled");
    }

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = RightDirection;
        Animators = GetComponentsInChildren<Animator>();
        BodyAnimator = Animators[0];
        ArmAnimator = Animators[1];
        Input = GetComponentInParent<PlayerInput>();
        DashCooltime = new WaitForSeconds(playerData.DashCooltime);
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = playerRigidBody.velocity;
        StateMachine.CurrentState.LogicUpdate();
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

    public void SetWallJumpVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    //public void SetInAirXVelocity(float xInput)
    //{
    //    workspace.Set(playerData.addedForce * xInput, 0f);
    //    playerRigidBody.AddForce(workspace);
        
    //    if (playerData.XVelocityLimit <= Mathf.Abs(CurrentVelocity.x))// velocity must be limited in this case
    //    {
    //        workspace.Set(playerData.XVelocityLimit * xInput, CurrentVelocity.y);
    //        playerRigidBody.velocity = workspace;
    //    }

    //    CurrentVelocity = playerRigidBody.velocity;
    //}


    public void PlayerWireDash()
    {
        if (CanDash)
        {
            CanDash = false;
            Input.UseDashInput();
            SetDashVelocity(Input.MovementInput.x);
            StartCoroutine(CountDashCooltime());
        }
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
        if (xInput != 0)
        {
            workspace.Set(playerData.DashForce * xInput + CurrentVelocity.x, CurrentVelocity.y);
        }
        else
        {
            workspace.Set(playerData.DashForce * FacingDirection + CurrentVelocity.x, CurrentVelocity.y);
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
        if (0f < xDirection)
        {
            if (FacingDirection != RightDirection)
            {
                Flip();
            }
        }
        else
        {
            if (FacingDirection != -RightDirection)
            {
                Flip();
            }
        }
    }

    public void CheckIfGrappled()
    {

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
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
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
    }

    #endregion
}
