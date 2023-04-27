using System;
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
    public BossReadyToShoot ReadyToShootState { get; private set; }
    public BossShootState ShootState { get; private set; }
    public BossCooldownState CooldownState { get; private set; }
    public BossEvadeState EvadeState { get; private set; }
    public BossRunAwayState RunAwayState { get; private set; }
    public BossQTEState QTEState { get; private set; }
    public BossQTEGotHitState QTEGotHitState { get; private set; }
    public BossEvadeToPhase2 EvadeToPhase2State { get; private set; }
    public BossExecutedState ExecutedState { get; private set; }
    public BossExecutedIdleState ExecutedIdleState { get; private set; }
    public BossFinalBeamState FinalBeamState { get; private set; }
    public BossFallingState FallingState { get; private set; }
    public BossAwakeAfterFallState AwakeAfterFallState { get; private set; }
    public BossAwakeIdleState AwakeIdleState { get; private set; }
    public BossDeadState DeadState { get; private set; }

    #endregion

    #region Other Components

    public Rigidbody2D bossRigidbody;

    public Animator BodyAnimator;
    public Animator HeadAnimator;

    public Transform bossInitialSpawnSpot;
    public Transform groundCheck;
    public Transform playerGrabPos;

    public FinishChecker finishChecker;
    public BossBullet bossBullet;

    #endregion

    #region Coroutine Optimization

    private IEnumerator _WaitIdleTime;
    private WaitForSeconds _waitidleTime;

    private IEnumerator _WaitShootTime;
    private WaitForSeconds _waitShootTime;

    private IEnumerator _WaitCooldownTime;
    private WaitForSeconds _waitCooldownTime;

    public Transform[] bossRunAwaySpots;

    #endregion

    #region Events

    public event Action OnShoot;
    public event Action OnFinalBeamShoot;

    #endregion

    #region Variables

    public int hitCount;
    private int hitPhase2Count;
    public bool isPhase1;
    public bool isBossReadyToBeExecuted { get; private set; }
    public bool isBossInFinishRange { get; set; }
    public bool isBossReadyToBeFinished { get; set; }

    private int rightDirection = 1;
    public int FacingDirection { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new BossStateMachine();
        bossData = GetComponent<BossData>();
        bossGunController = GetComponentInChildren<BossGunController>();

        BodyAnimator = GetComponent<Animator>();


        AppearState = new BossAppearState(this, StateMachine, bossData, "appear");
        IdleState = new BossIdleState(this, StateMachine, bossData, "idle");
        AimingState = new BossAimingState(this, StateMachine, bossData, "aiming");
        AimLockState = new BossAimLockState(this, StateMachine, bossData, "aimLock");
        ReadyToShootState = new BossReadyToShoot(this, StateMachine, bossData, "readyToShoot");
        ShootState = new BossShootState(this, StateMachine, bossData, "shoot");
        CooldownState = new BossCooldownState(this, StateMachine, bossData, "cooldown");
        EvadeState = new BossEvadeState(this, StateMachine, bossData, "evade");
        RunAwayState = new BossRunAwayState(this, StateMachine, bossData, "runAway");
        QTEState = new BossQTEState(this, StateMachine, bossData, "QTE");
        QTEGotHitState = new BossQTEGotHitState(this, StateMachine, bossData, "QTEGotHit");
        EvadeToPhase2State = new BossEvadeToPhase2(this, StateMachine, bossData, "toPhase2");
        ExecutedState = new BossExecutedState(this, StateMachine, bossData, "executed");
        ExecutedIdleState = new BossExecutedIdleState(this, StateMachine, bossData, "executedIdle");
        FinalBeamState = new BossFinalBeamState(this, StateMachine, bossData, "finalBeam");
        FallingState = new BossFallingState(this, StateMachine, bossData, "falling");
        AwakeAfterFallState = new BossAwakeAfterFallState(this, StateMachine, bossData, "awakeAfterFall");
        AwakeIdleState = new BossAwakeIdleState(this, StateMachine, bossData, "awakeIdle");
        DeadState = new BossDeadState(this, StateMachine, bossData, "dead");
        
    }

    private void OnEnable()
    {
        GameManager.Instance.bossController = this;
        GameManager.Instance.playerGrabBossPos = playerGrabPos;
        GameManager.Instance.playerController.OnQTEHit -= ChangeToQTEGotHitState;
        GameManager.Instance.playerController.OnQTEHit += ChangeToQTEGotHitState;
    }

    private void Start()
    {
        bossRigidbody = GetComponent<Rigidbody2D>();

        _WaitIdleTime = WaitIdleTime();
        _waitidleTime = new WaitForSeconds(bossData.idleWaitTime);

        _WaitShootTime = WaitShootTime();
        _waitShootTime = new WaitForSeconds(bossData.shootWaitTime);

        _WaitCooldownTime = WaitCooldownTime();
        _waitCooldownTime = new WaitForSeconds(bossData.cooldownWaitTime);


        isPhase1 = true;
        //hitCount = 1;
        hitCount = 2; // test
        hitPhase2Count = bossData.hitPhase2Count;
        FacingDirection = rightDirection;

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

    #region Other Functions

    private void ChangeToQTEGotHitState()
    {
        GameManager.Instance.playerController.camShake.TurnOnShake(0.1f, 0.2f);
        StateMachine.ChangeState(QTEGotHitState);
    }

    public void BossRunAway()
    {
        if (isPhase1 == false && hitPhase2Count < hitCount)
        {
            transform.position = bossRunAwaySpots[bossRunAwaySpots.Length-1].position;
            isBossReadyToBeExecuted = true;
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, bossRunAwaySpots.Length - 1);
            transform.position = bossRunAwaySpots[randomIndex].position;
            
        }
        
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Vector3 newScale = Vector3.one;
        newScale.x = FacingDirection;
        transform.localScale = newScale;
    }
    public void TurnOffCeilingCollider()
    {
        GameManager.Instance.bossCanvasController.TurnOffCeilingCollider();
        ChangeToFallingState();
    }
    
    #endregion


    #region Coroutines

    #region bossIdleState

    public void StartWaitIdleTime()
    {
        StartCoroutine(_WaitIdleTime);
    }

    public void StopWaitIdleTime()
    {
        StopCoroutine(_WaitIdleTime);
    }

    private IEnumerator WaitIdleTime()
    {
        while (true)
        {
            yield return _waitidleTime;
            StateMachine.ChangeState(AimingState);
            StopWaitIdleTime();
        }
        
    }

    #endregion

    #region bossShootState

    public void StartWaitShootTime()
    {
        _WaitShootTime = WaitShootTime();
        StartCoroutine(_WaitShootTime);
    }

    public void StopWaitShootTime()
    {
        StopCoroutine(_WaitShootTime);
    }

    private IEnumerator WaitShootTime()
    {
        yield return _waitShootTime;
        StateMachine.ChangeState(CooldownState);
        StopCoroutine(_WaitShootTime);
    }

    #endregion

    #region bossCooldownState

    public void StartWaitCooldownTime()
    {
        _WaitCooldownTime = WaitCooldownTime();
        StartCoroutine(_WaitCooldownTime);
    }

    public void StopWaitCooldownTime()
    {
        StopCoroutine(_WaitCooldownTime);
    }

    private IEnumerator WaitCooldownTime()
    {
        yield return _waitCooldownTime;
        StateMachine.ChangeState(AimingState);
        StopCoroutine(_WaitCooldownTime);
    }

    #endregion

    #region bossExecutedIdleState

    public void StartWaitAndChangeToFinalBeamState()
    {
        StartCoroutine(WaitAndChangeToFinalBeamState());
    }
    private IEnumerator WaitAndChangeToFinalBeamState()
    {
        yield return new WaitForSeconds(5f);
        StateMachine.ChangeState(FinalBeamState);
    }

    #endregion

    #endregion


    #region Set Functions

    public void SetBossVelocity(Vector2 velocity)
    {
        bossRigidbody.velocity = velocity;
    }



    #endregion

    #region CheckFunctions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, bossData.groundCheckRadius, bossData.whatIsGround);
    }

    public void CheckIfShouldFlip()
    {
        if (1f <= FacingDirection) // looking right
        {
            if (GameManager.Instance.playerController.transform.position.x <= transform.position.x) // if player is on left side of boss
            {
                Flip();
            }
        }
        else // looking left
        {
            if (transform.position.x <= GameManager.Instance.playerController.transform.position.x) // if player is on the right side of boss
            {
                Flip();
            }
        }
    }
    

    public bool CheckIfPhase1()
    {
        if (isPhase1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckIfQTE()
    {
        if (hitPhase2Count <= hitCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Animation Call Events

    public void ChangeToShootState()
    {
        StateMachine.ChangeState(ShootState);
    }

    public void OnShootState()
    {
        OnShoot?.Invoke();
    }

    public void ChangeToRunAwayState()
    {
        StateMachine.ChangeState(RunAwayState);
    }

    public void ChangeToCooldownState()
    {
        StateMachine.ChangeState(CooldownState);    
    }

    public void ChangeToExecutedIdleState()
    {
        StateMachine.ChangeState(ExecutedIdleState);
    }

    public void ChangeToFallingState()
    {
        StateMachine.ChangeState(FallingState);
    }

    public void ChangeToAwakeIdleState()
    {
        StateMachine.ChangeState(AwakeIdleState);
    }
    public void InvokeOnFinalBeamShoot()
    {
        OnFinalBeamShoot?.Invoke();
        //bossBullet.ShootFinalBullet();
    }

    public void TurnOnGetHitCamShake()
    {
        GameManager.Instance.playerController.camShake.TurnOnShake(GameManager.Instance.playerController.camShake.QTEHitShakeTime, GameManager.Instance.playerController.camShake.QTEHitShakeIntensity);
    }

    #endregion

    
}
