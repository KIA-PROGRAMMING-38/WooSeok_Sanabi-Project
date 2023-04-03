using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public Rigidbody2D playerRigidBody { get; private set; }

    private Animator[] Animators; // GetComponentInChildren 하면 자기꺼도 가져와서 걍 배열로 가져왔음
    public Animator BodyAnimator { get; private set; }
    public Animator ArmAnimator { get; private set; }   
    public PlayerInput Input { get; private set; }

    [SerializeField] private PlayerData playerData;

    public Vector2 CurrentVelocity { get; private set; }    
    private Vector2 workspace;
    public int FacingDirection { get; private set; }

    private int RightDirection = 1;

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        RunState = new PlayerRunState(this, StateMachine, playerData, "run");
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
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckIfShouldFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }
}
