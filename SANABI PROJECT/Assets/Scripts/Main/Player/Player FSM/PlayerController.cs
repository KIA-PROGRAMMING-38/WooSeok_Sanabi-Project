using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
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
    public PlayerDamagedState DamagedState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerDamagedDashState DamagedDashState { get; private set; }
    public PlayerRollingState RollingState { get; private set; }
    public PlayerApproachDash ApproachDash { get; private set; }
    public PlayerExecuteHolded ExecuteHolded { get; private set; }
    public PlayerExecuteDash ExecuteDash { get; private set; }

    public PlayerGetHitState GetHitState { get; private set; }
    public PlayerQTEState QTEState { get; private set; }
    public PlayerQTEHitState QTEHitState { get; private set; }
    public PlayerEvadeToPhase2State EvadeToPhase2State { get; private set; }
    public PlayerExecuteBossState ExecuteBossState { get; private set; }
    public PlayerMenaceState MenaceState { get; private set; }
    public PlayerEvadeBeamState EvadeBeamState { get; private set; }
    public PlayerFinishBossState FinishBossState { get; private set; }
    public PlayerPausedState PausedState { get; private set; }
    

    #endregion

    public Rigidbody2D playerRigidBody { get; private set; }
    public Animator BodyAnimator { get; private set; }
    public Animator ArmAnimator { get; private set; }

    public Animator ExecuteDashIconAnimator;

    public Animator BodyEffector;
    public Animator ArmEffector;
    public PlayerInput Input { get; private set; }
    public PlayerArmController ArmController { get; private set; }

    public GrabController GrabController;

    public HPRobotController HPBarController;

    public ExecuteDashIconController ExecuteDashIconController;

    public Transform armTransform;

    public PlayerData playerData;

    public PlayerHealth playerHealth;

    public ShakeCamera camShake;

    public CameraFollow camFollow;

    public Transform playerTransform;
    //public GameManager gameManager;

    public TimeSlow timeSlower;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;

    public PlayerAfterImage spritePrefab;

    #region variables
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    public int FacingDirection { get; private set; }
    private int RightDirection = 1; // to avoid magicNumber

    public bool stillOnWall;

    //private int PlatformLayerNumber;
    private int MagmaLayerNumber;
    private int NormalWallLayerNumber;
    private int NoGrabWallLayerNumber;
    private bool isPlayerDamaged;
    private bool isPlayerInvincible;
    private float playerInvincibleTime;
    private WaitForSeconds playerInvincibleWaitTime;


    private WaitForSeconds DashCooltime;
    private bool CanDash = true;
    private Vector2 distanceVec;
    private Vector2 perpendicularVec;
    private Vector2 direction;
    private Vector2 DamagedDirection;

    public bool isDashing = false;
    private float dashTimeLeft;

    private ObjectPool<PlayerAfterImage> WireDashPool;
    public event Action OnDamagedDash;
    public event Action OnApproachDashToTurret;
    public event Action OnExecuteDash;
    public event Action OnWireDash;
    public event Action OnWireDashFinished;

    private float executeHoldMaxTime;
    private WaitForSeconds _executeHoldMaxTime;
    private IEnumerator _HoldOnToTurret;

    public event Action OffTurret;
    private IEnumerator _ShowAfterImage;
    private float afterImageGapTime;
    private WaitForSeconds _afterImageGapTime;
    [SerializeField] private Transform JumpEffectorTransform;
    [SerializeField] private Transform LandEffectorTransform;
    [SerializeField] private Transform WallJumpEffectorTransform;
    public Transform WallSlideEffectorTransform;
    [SerializeField] private Transform WireShootEffectorTransform;
    [SerializeField] private Transform ExecuteHoldedEffectorTransform;

    private ObjectPool<WallSlideDust> dustPool;
    public WallSlideDust dustPrefab;
    private IEnumerator _ShowWallSlideDust;
    [SerializeField] private float dustCreateTime = 0.05f;
    private WaitForSeconds _dustCreateTime;


    public event Action OnApproachDashToBoss;

    public bool isPlayerBossState { get; set; }
    public event Action OnGetHit;
    public event Action OnQTE;
    public event Action OnQTEHit;
    public event Action OnQTEHitFinished;

    private IEnumerator _WaitAndChangeToEvadeBeamState;
    private WaitForSeconds _menaceWaitTime;

    public event Action OnFinishBoss;

    
    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        //playerData = GetComponentInParent<PlayerData>();
        playerData = GameManager.Instance.playerData;
        //ArmController = GameObject.FindGameObjectWithTag("Arm").GetComponent<PlayerArmController>();
        ArmController = GameManager.Instance.armController;
        camShake = Camera.main.GetComponent<ShakeCamera>();
        camFollow = Camera.main.GetComponent<CameraFollow>();
        playerTransform = GetComponent<Transform>();    
        //gameManager = new GameManager();
        
        playerHealth = GetComponentInParent<PlayerHealth>();

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
        DamagedState = new PlayerDamagedState(this, StateMachine, playerData, "damaged");
        DeadState = new PlayerDeadState(this, StateMachine, playerData, "dead");
        DamagedDashState = new PlayerDamagedDashState(this, StateMachine, playerData, "damagedDash");
        RollingState = new PlayerRollingState(this, StateMachine, playerData, "rolling");
        ApproachDash = new PlayerApproachDash(this, StateMachine, playerData, "approachDash");
        ExecuteHolded = new PlayerExecuteHolded(this, StateMachine, playerData, "executeHolded");
        ExecuteDash = new PlayerExecuteDash(this, StateMachine, playerData, "executeDash");
        GetHitState = new PlayerGetHitState(this, StateMachine, playerData, "getHit");
        QTEState = new PlayerQTEState(this, StateMachine, playerData, "QTE");
        QTEHitState = new PlayerQTEHitState(this, StateMachine, playerData, "QTEHit");
        EvadeToPhase2State = new PlayerEvadeToPhase2State(this,StateMachine, playerData, "evadeToPhase2");
        ExecuteBossState = new PlayerExecuteBossState(this, StateMachine, playerData, "executeBoss");
        MenaceState = new PlayerMenaceState(this, StateMachine, playerData, "menace");
        EvadeBeamState = new PlayerEvadeBeamState(this, StateMachine, playerData, "evadeBeam");
        FinishBossState = new PlayerFinishBossState(this, StateMachine,playerData, "finishBoss");
        PausedState = new PlayerPausedState(this, StateMachine, playerData, "paused");
    }

    private void Start()
    {
        transform.position = GameManager.Instance.playerSpawnSpot.position;
        playerRigidBody = GetComponent<Rigidbody2D>();
        FacingDirection = RightDirection;
        _ShowAfterImage = ShowAfterImage();
        //Animators = GetComponentsInChildren<Animator>();
        BodyAnimator = GetComponent<Animator>();
        ArmAnimator = GameObject.FindGameObjectWithTag("Arm").GetComponent<Animator>();
        ExecuteDashIconController = GetComponentInChildren<ExecuteDashIconController>();
        afterImageGapTime = playerData.afterImageGapTime;
        _afterImageGapTime = new WaitForSeconds(afterImageGapTime);
        _dustCreateTime = new WaitForSeconds(dustCreateTime);
        _WaitAndChangeToEvadeBeamState = WaitAndChangeToEvadeBeamState();
        _menaceWaitTime = new WaitForSeconds(playerData.menaceWaitTime);


        Input = GetComponentInParent<PlayerInput>();
        DashCooltime = new WaitForSeconds(playerData.DashCoolDown);
        WireDashPool = new ObjectPool<PlayerAfterImage>(CreateWireDashSprite, OnGetSpriteFromPool, OnReturnSpriteToPool);
        dustPool = new ObjectPool<WallSlideDust>(CreateWallSlideDust, OnGetDustFromPool, OnReturnDustToPool);
        playerInvincibleTime = playerData.invincibleTime;
        playerInvincibleWaitTime = new WaitForSeconds(playerInvincibleTime);
        executeHoldMaxTime = playerData.executeHoldMaxTime;
        _executeHoldMaxTime = new WaitForSeconds(executeHoldMaxTime);
        _HoldOnToTurret = HoldOnToTurret();
        _ShowWallSlideDust = ShowWallSlideDust();

        NormalWallLayerNumber = LayerMask.NameToLayer("NormalWall");
        NoGrabWallLayerNumber = LayerMask.NameToLayer("NoGrabWall");
        MagmaLayerNumber = LayerMask.NameToLayer("Magma");


        StateMachine.Initialize(IdleState);

        //Debug.Log($"플레이어컨트롤러에서의 ID = {playerHealth.GetInstanceID()}");

    }
    private void Update()
    {
        CurrentVelocity = playerRigidBody.velocity;
        StateMachine.CurrentState.LogicUpdate();

        //Debug.Log($"현재 플레이어 hp = {playerHealth.GetCurrentHp()}");
        //Debug.Log(CurrentVelocity);
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

    public void ClearDustPool()
    {
        dustPool.ClearPool();
    }

    public void ClearAfterImagePool()
    {
        WireDashPool.ClearPool();
    }

    public void PlayerWireDash()
    {
        if (CanDash)
        {
            GameManager.Instance.audioManager.Play("playerWireDash");
            OnWireDash?.Invoke();
            StartShowAfterImage();
            CanDash = false;
            //isDashing = true;
            PlayerIsDash(true);
            //dashTimeLeft = playerData.DashTime;
            Input.UseDashInput();
            SetDashVelocity(Input.MovementInput.x);
            StartCoroutine(CountDashCooltime());
            
        }
    }
    public void InvokeOnFinishBoss()
    {
        camShake.TurnOnShake(camShake.finishBossShakeTime, camShake.finishBossShakeIntensity);
        timeSlower.PleaseSlowDown(playerData.finishBossTimeScale, playerData.finishBossSlowTime);
        OnFinishBoss?.Invoke();
    }
    //public void PlayerApproachDash()
    //{
    //    if (CanDash)
    //    {
    //        CanDash = false;
    //        PlayerIsDash(true);
    //        dashTimeLeft = playerData.DashTime;
    //        Input.UseDashInput();
    //        StartCoroutine(CountDashCooltime());
    //    }
    //}

    public void InvokeOnQTEHitFinished()
    {
        OnQTEHitFinished?.Invoke();
    }

    public void InvokeOnQTEHit()
    {
        OnQTEHit?.Invoke();
        camShake.TurnOnShake(camShake.QTEHitShakeTime, camShake.QTEHitShakeIntensity);
        //camShake.TurnOnShake(cameraShakeTime, cameraShakeIntensity);
    }

    public void TurnOnGetHitCamShake()
    {
        camShake.TurnOnShake(camShake.QTEHitShakeTime, camShake.QTEHitShakeIntensity);
    }

    public void InvokeOnGetHit()
    {
        OnGetHit?.Invoke();
    }

    public void InvokeOnQTE()
    {
        OnQTE?.Invoke();
    }

    public void TurretHasBeenReleased()
    {
        OffTurret?.Invoke();
    }

    public void PlayerIsDash(bool isPlayerDashing)
    {
        isDashing = isPlayerDashing;
    }

    public void StartShowAfterImage()
    {
        _ShowAfterImage = ShowAfterImage();
        dashTimeLeft = playerData.DashTime;
        StartCoroutine(_ShowAfterImage);
    }

    public void StopShowAfterImage()
    {
        StopCoroutine(_ShowAfterImage);
    }

    private IEnumerator ShowAfterImage()
    {
        while (0f <= dashTimeLeft)
        {
            //dashTimeLeft -= Time.deltaTime;
            dashTimeLeft -= afterImageGapTime;
            WireDashPool.GetFromPool();
            yield return _afterImageGapTime;
        }
        StopCoroutine(_ShowAfterImage);
    }

    public PlayerAfterImage CreateWireDashSprite()
    {
        PlayerAfterImage sprite = Instantiate(spritePrefab);
        sprite.WireDashPool = WireDashPool;
        return sprite;
    }

    public WallSlideDust CreateWallSlideDust()
    {
        WallSlideDust dust = Instantiate(dustPrefab);
        dust.dustPool = dustPool;
        return dust;
    }

    public void StartShowWallSlideDust()
    {
        //if (null != _ShowWallSlideDust)
        //    StopCoroutine(_ShowWallSlideDust);
        _ShowWallSlideDust = ShowWallSlideDust();
        StartCoroutine(_ShowWallSlideDust);
    }

    public void StopShowWallSlideDust()
    {
        //_ShowWallSlideDust = ShowWallSlideDust();
        StopCoroutine(_ShowWallSlideDust);
    }

    private IEnumerator ShowWallSlideDust()
    {
        while (true)
        {
            dustPool.GetFromPool();
            yield return _dustCreateTime;
        }
    }

    public void StartWaitAndChangeToEvadeBeamState()
    {
        //StartCoroutine(WaitAndChangeToEvadeBeamState());
        StartCoroutine(_WaitAndChangeToEvadeBeamState);
    }

    private IEnumerator WaitAndChangeToEvadeBeamState()
    {
        yield return _menaceWaitTime;
        StateMachine.ChangeState(EvadeBeamState);
    }

    public void PlayerWireDashStop()
    {
        OnWireDashFinished?.Invoke();
        StopCoroutine(CountDashCooltime());
        CanDash = true;
    }

    private IEnumerator CountDashCooltime()
    {
        yield return DashCooltime;
        CanDash = true;

        OnWireDashFinished?.Invoke();
    }

    public void IgnorePlatformCollision(bool ignoreplatform)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, NormalWallLayerNumber, ignoreplatform);
        Physics2D.IgnoreLayerCollision(gameObject.layer, NoGrabWallLayerNumber, ignoreplatform);
        Physics2D.IgnoreLayerCollision(gameObject.layer, MagmaLayerNumber, ignoreplatform);
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

    public void StartExecuteHolded()
    {
        _HoldOnToTurret = HoldOnToTurret(); // to renew the coroutine preventing it from cotinuing from the moment i stopped
        StartCoroutine(_HoldOnToTurret);
    }

    public void StopExecuteHolded()
    {
        StopCoroutine(_HoldOnToTurret);

    }

    private IEnumerator HoldOnToTurret()
    {
        while (true)
        {
            yield return _executeHoldMaxTime;
            StateMachine.ChangeState(ExecuteDash);
            StopCoroutine(_HoldOnToTurret);
            yield return null;
        }

    }

    public void AddXVelocityWhenGrappled(float xInput)
    {
        workspace.Set(playerData.grappleAddedForce * xInput, 0f);
        playerRigidBody.AddForce(workspace);
        CurrentVelocity = playerRigidBody.velocity;
    }

    public void SetDamagedDashVelocity(float inputX, float inputY, float damagedDashForce)
    {
        workspace.Set(inputX, inputY);
        workspace = workspace.normalized * damagedDashForce;
        DamagedDirection = workspace;
    }

    public void SetMinMaxVelocityY()
    {
        workspace.Set(CurrentVelocity.x, Mathf.Clamp(CurrentVelocity.y, playerData.playerMinVelocityY, playerData.playerMaxVelocityY));
        playerRigidBody.velocity = workspace;
        CurrentVelocity = workspace;
    }


    public Vector2 GetDamagedDashVelocity()
    {
        return DamagedDirection;
    }

    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(float xInput)
    {
        //if (xInput != 0 && xInput != FacingDirection)
        //{
        //    Flip();
        //}
        if (xInput != 0 && xInput * FacingDirection < 0f)
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

    public void ResetDamageState()
    {
        isPlayerDamaged = false;
        ArmController.IsPlayerDamaged = isPlayerDamaged;
        HPBarController.IsPlayerDamaged = isPlayerDamaged;
    }


    public bool CheckIfDamaged()
    {
        return isPlayerDamaged;
    }

    public void MakePlayerInvicible()
    {
        isPlayerInvincible = true;
    }

    public void MakePlayerVulnerable()
    {
        isPlayerInvincible = false;
    }
    public IEnumerator MakePlayerInvincibleForCetainTime()
    {
        MakePlayerInvicible();
        yield return playerInvincibleWaitTime;
        MakePlayerVulnerable();
    }

    public void ChangeToInAirState()
    {
        StateMachine.ChangeState(InAirState);
    }

    public void InvokeOnEvadeBeam()
    {
        GameManager.Instance.bossController.TurnOffCeilingCollider();
        StateMachine.ChangeState(InAirState);
    }

    private void InvokeOnDamagedDash() // to be called from animation frames as event
    {
        OnDamagedDash?.Invoke();
    }

    private void InvokeOnExecuteDash()
    {
        OnExecuteDash?.Invoke();
    }

    public void ChangeToPausedState()
    {
        StateMachine.ChangeState(PausedState);
    }

    public void CheckIfShouldRotate(float xInput, float yInput)
    {
        float rotationAngle = 45f;
        // should consider that this function will be called after CheckIfShouldFlip()
        if (1f <= yInput) // if pressed Up
        {
            if (1f <= xInput)
            {
                transform.Rotate(0f, 0f, rotationAngle);
            }
            else if (xInput <= -1f)
            {
                transform.Rotate(0f, 0f, -rotationAngle);
            }
            else
            {
                transform.Rotate(0f, 0f, rotationAngle * 2);
            }
        }
        else if (yInput <= -1f) // if pressed Down
        {
            if (1f <= xInput)
            {
                transform.Rotate(0f, 0f, -rotationAngle);
            }
            else if (xInput <= -1f)
            {
                transform.Rotate(0f, 0f, +rotationAngle);
            }
            else
            {
                transform.Rotate(0f, 0f, rotationAngle * 2);
            }
        }

    }

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public void TransformMove(Vector2 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void ChangeToIdleState()
    {
        StateMachine.ChangeState(IdleState);
    }

    public void ChangeToMenaceState()
    {
        StateMachine.ChangeState(MenaceState);
    }
    public void Flip()
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

    private void OnGetDustFromPool(WallSlideDust dust) => dust.gameObject.SetActive(true);
    private void OnReturnDustToPool(WallSlideDust dust) => dust.gameObject.SetActive(false);

    #endregion

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.layer == MagmaLayerNumber)
    //    {
    //        if (!isPlayerInvincible)
    //        {
    //            StartCoroutine(MakePlayerInvincibleForCetainTime());
    //            isPlayerDamaged = true;
    //            ArmController.IsPlayerDamaged = isPlayerDamaged;
    //            HPBarController.IsPlayerDamaged = isPlayerDamaged;
    //        }
    //    }
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == MagmaLayerNumber)
        {
            if (!isPlayerInvincible)
            {
                StartCoroutine(MakePlayerInvincibleForCetainTime());
                isPlayerDamaged = true;
                ArmController.IsPlayerDamaged = isPlayerDamaged;
                HPBarController.IsPlayerDamaged = isPlayerDamaged;
            }
        }
    }

    private bool isPlayerHitDeathPlatform;
    public bool CheckIfPlayerHitDeathPlatform()
    {
        return isPlayerHitDeathPlatform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Turret"))
        {
            OnApproachDashToTurret?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            OnApproachDashToBoss?.Invoke();
        }
        else if (collision.gameObject.CompareTag("TurretBullet") || collision.gameObject.CompareTag("BossBullet"))
        {
            if (!isPlayerInvincible)
            {
                StartCoroutine(MakePlayerInvincibleForCetainTime());
                isPlayerDamaged = true;
                ArmController.IsPlayerDamaged = isPlayerDamaged;
                HPBarController.IsPlayerDamaged = isPlayerDamaged;
            }
        }
        else if (collision.gameObject.CompareTag("DeathPlatform"))
        {
            isPlayerHitDeathPlatform = true;
        }

    }


    #region Effects
    public void SetJumpEffectOn()
    {
        BodyEffector.gameObject.transform.position = JumpEffectorTransform.position;
        BodyEffector.SetTrigger("jump");
    }

    public void SetLandEffectOn()
    {
        BodyEffector.gameObject.transform.position = LandEffectorTransform.position;
        BodyEffector.SetTrigger("land");
    }

    public void SetWallJumpEffectOn()
    {
        BodyEffector.gameObject.transform.position = WallJumpEffectorTransform.position;
        WallJumpEffectorTransform.localScale = transform.localScale;
        BodyEffector.gameObject.transform.localScale = WallJumpEffectorTransform.localScale;
        BodyEffector.gameObject.transform.rotation = WallJumpEffectorTransform.rotation;
        BodyEffector.SetTrigger("wallJump");
    }

    public void SetWireShootEffectOn()
    {
        ArmEffector.gameObject.transform.position = WireShootEffectorTransform.position;
        WireShootEffectorTransform.localScale = transform.localScale;
        ArmEffector.gameObject.transform.localScale = WireShootEffectorTransform.localScale;
        ArmEffector.gameObject.transform.rotation = WireShootEffectorTransform.rotation;
        ArmEffector.SetTrigger("wireShoot");
    }

    public void SetExecuteHoldedEffectOn()
    {
        BodyEffector.gameObject.transform.position = ExecuteHoldedEffectorTransform.position;
        BodyEffector.SetTrigger("executeHolded");
    }

    public void SetExecuteDashEffectOn()
    {
        BodyEffector.gameObject.transform.position = ExecuteHoldedEffectorTransform.position;
        BodyEffector.SetTrigger("executeDash");
    }
    #endregion
}
