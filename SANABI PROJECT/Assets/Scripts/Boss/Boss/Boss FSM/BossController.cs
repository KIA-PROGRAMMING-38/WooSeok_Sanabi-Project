using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BossStateMachine StateMachine { get; private set; }
    public BossData bossData { get; private set; }
    public BossGunController bossGunController { get; private set; }

    #region Boss States

    public BossAppearState AppearState { get; private set; }
    public BossIdleState IdleState { get; private set; }
    public BossAimingState AimingState { get; private set; }
    public BossAimLockState AimLockState { get; private set; }
    public BossShootState ShootState { get; private set; }
    public BossCooldownState CooldownState { get; private set; }
    public BossEvadeState EvadeState { get; private set; }
    public BossQTEState QTEState { get; private set; }
    public BossExecutedState ExecutedState { get; private set; }
    public BossDeadState DeadState { get; private set; }

    #endregion

    #region Other Components

    public Rigidbody2D bossRigidbody;

    public Animator BodyAnimator;
    public Animator HeadAnimator;

    public Transform bossSpawnSpot;
    public Transform groundCheck;

    #endregion

    private void Awake()
    {
        StateMachine = new BossStateMachine();
        bossData = GetComponent<BossData>();
        bossGunController = GetComponentInChildren<BossGunController>();

        BodyAnimator= GetComponent<Animator>();
        HeadAnimator= GetComponentInChildren<Animator>();


        AppearState = new BossAppearState(this, StateMachine, bossData, "appear");
        IdleState = new BossIdleState(this, StateMachine, bossData, "idle");
        AimingState = new BossAimingState(this, StateMachine, bossData, "aiming");
        AimLockState = new BossAimLockState(this, StateMachine, bossData, "aimLock");
        ShootState = new BossShootState(this, StateMachine, bossData, "shoot");
        CooldownState = new BossCooldownState(this, StateMachine, bossData, "cooldown");
        EvadeState = new BossEvadeState(this, StateMachine, bossData, "evade");
        QTEState = new BossQTEState(this, StateMachine, bossData, "QTE");
        ExecutedState = new BossExecutedState(this, StateMachine, bossData, "executed");
        DeadState = new BossDeadState(this, StateMachine, bossData, "dead");
    }


    private void Start()
    {
        bossRigidbody = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(AppearState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        //Debug.Log(StateMachine.CurrentState);
    }


    #region Set Functions

    public void SetBossVelocity(Vector2 velocity)
    {
        bossRigidbody.velocity = velocity;
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, bossData.groundCheckRadius, bossData.whatIsGround);
    }

    #endregion


}
