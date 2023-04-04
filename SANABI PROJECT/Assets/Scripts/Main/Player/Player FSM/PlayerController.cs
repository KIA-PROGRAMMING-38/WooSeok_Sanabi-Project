using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    #region playerStates 
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    // public PlayerFallState FallState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    #endregion
    public Rigidbody2D playerRigidBody { get; private set; }

    private Animator[] Animators; // GetComponentInChildren 하면 자기꺼도 가져와서 걍 배열로 가져왔음
    public Animator BodyAnimator { get; private set; }
    public Animator ArmAnimator { get; private set; }   
    public PlayerInput Input { get; private set; }

    public PlayerData playerData;

    [SerializeField] private Transform groundCheck;

    #region variables
    public Vector2 CurrentVelocity { get; private set; }    
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    private int RightDirection = 1; // to avoid magicNumber
    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        playerData = GetComponent<PlayerData>();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        RunState = new PlayerRunState(this, StateMachine, playerData, "run");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        // FallState = new PlayerFallState(this, StateMachine, playerData, "fall");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
    }

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = RightDirection;
        Animators = GetComponentsInChildren<Animator>();
        BodyAnimator = Animators[0];
        ArmAnimator = Animators[1];   
        Input = GetComponent<PlayerInput>();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = playerRigidBody.velocity;
        StateMachine.CurrentState.LogicUpdate();
        Debug.Log(StateMachine.CurrentState);
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
    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
        // this will return true if anything conditioned above is detected, otherwise false
    }
    #endregion

    #region Other Functions
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();


    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }

    #endregion
}
