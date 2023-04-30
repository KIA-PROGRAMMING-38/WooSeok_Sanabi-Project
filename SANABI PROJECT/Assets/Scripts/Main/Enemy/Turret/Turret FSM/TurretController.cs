using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public TurretStateMachine StateMachine { get; private set; }
    public TurretData turretData { get; private set; }

    #region States

    public TurretPopUpState PopUpState { get; private set; }
    public TurretCooldownState CooldownState { get; private set; }
    public TurretAimingState AimingState { get; private set; }
    public TurretAimLockState AimLockState { get; private set; }
    public TurretShootState ShootState { get; private set; }
    public TurretExecuteHoldedState ExecuteHoldedState { get; private set; }
    public TurretDeadState DeadState { get; private set; }

    #endregion
    public Animator BodyAnimator { get; private set; }
    public Animator GunAnimator;
    public Animator StageAnimator;
    public Animator WarningInsideAnimator;
    public Animator WarningOutsideAnimator;
    private BoxCollider2D turretCollider;
    public CircleCollider2D blockerCollider;

    public PlayerController playerController;
    public GunController gunController;
    public GrabController grabController;
    public TurretSpawner turretSpawner;

    public IEnumerator _StayAiming;
    private WaitForSeconds _aimTime;
    private float aimTime;
    public ObjectPool<TurretBullet> turretBulletPool { get; private set; }
    public TurretBullet bulletPrefab;


    private float minRandomAngle;
    private float maxRandomAngle;
    private float shootGapTime;
    private float randomShootAngle;
    private Vector3 rotation;
    private Quaternion bulletRotation;
    private WaitForSeconds _shootGapTime;
    private IEnumerator _ShootMultipleBullets;

    private int shotBulletNumber;
    //[SerializeField] private int shotBulletMaxNumber = 15;
    private int shotBulletMaxNumber;
    public event Action OnFinishedShooting;

    private WaitForSeconds _cooldownTime;
    //[SerializeField] private float cooldownTime = 0.5f;
    private float cooldownTime;
    private IEnumerator _WaitForCooldown;

    private bool isTurretGrabbed;
    public event Action OnFinishedCooldown;

    private float aimLockTime;
    private WaitForSeconds _aimLockTime;
    private IEnumerator _StayAimLock;

    public ObjectPool<TurretBrokenParts> brokenPartsObjectPool { get; private set; }
    [SerializeField] TurretBrokenParts[] brokenPartsPrefabs;
    [SerializeField] private int howManyBrokenParts = 10;

    [SerializeField] private Transform playerHoldPosition;
    public event Action onTurretDeath;

    [SerializeField] private SpriteRenderer WarningIconInsideRenderer;
    [SerializeField] private SpriteRenderer WarningIconOutsideRenderer;

    private void Awake()
    {
        //StateMachine = new TurretStateMachine();
        //turretData = GetComponentInParent<TurretData>();
        //BodyAnimator = GetComponent<Animator>();



        //PopUpState = new TurretPopUpState(this, StateMachine, turretData, "popUp");
        //CooldownState = new TurretCooldownState(this, StateMachine, turretData, "coolDown");
        //AimingState = new TurretAimingState(this, StateMachine, turretData, "aiming");
        //ShootState = new TurretShootState(this, StateMachine, turretData, "shoot");
        //ExecuteHoldedState = new TurretExecuteHoldedState(this, StateMachine, turretData, "executeHolded");
        //DeadState = new TurretDeadState(this, StateMachine, turretData, "dead");
    }

    private void OnEnable()
    {
        //playerController = GameManager.Instance.playerController;
        //grabController = GameManager.Instance.grabController;
        //StateMachine = new TurretStateMachine();
        //turretData = GetComponentInParent<TurretData>();
        //BodyAnimator = GetComponent<Animator>();



        //PopUpState = new TurretPopUpState(this, StateMachine, turretData, "popUp");
        //CooldownState = new TurretCooldownState(this, StateMachine, turretData, "coolDown");
        //AimingState = new TurretAimingState(this, StateMachine, turretData, "aiming");
        //ShootState = new TurretShootState(this, StateMachine, turretData, "shoot");
        //ExecuteHoldedState = new TurretExecuteHoldedState(this, StateMachine, turretData, "executeHolded");
        //DeadState = new TurretDeadState(this, StateMachine, turretData, "dead");
    }

    private void Start()
    {
        //gameObject.SetActive(true); // test 용
        playerController = GameManager.Instance.playerController;
        grabController = GameManager.Instance.grabController;
        turretSpawner = GameManager.Instance.turretSpawner;
        StateMachine = new TurretStateMachine();
        turretData = GetComponentInParent<TurretData>();
        BodyAnimator = GetComponent<Animator>();



        PopUpState = new TurretPopUpState(this, StateMachine, turretData, "popUp");
        CooldownState = new TurretCooldownState(this, StateMachine, turretData, "coolDown");
        AimingState = new TurretAimingState(this, StateMachine, turretData, "aiming");
        AimLockState = new TurretAimLockState(this, StateMachine, turretData, "aiming");
        ShootState = new TurretShootState(this, StateMachine, turretData, "shoot");
        ExecuteHoldedState = new TurretExecuteHoldedState(this, StateMachine, turretData, "executeHolded");
        DeadState = new TurretDeadState(this, StateMachine, turretData, "dead");
        turretBulletPool = new ObjectPool<TurretBullet>(CreateBullet, OnGetBulletFromPool, OnReturnBulletToPool);
        brokenPartsObjectPool = new ObjectPool<TurretBrokenParts>(CreateBrokenParts, OnGetBrokenPartsFromPool, OnReturnBrokenPartsToPool);
        turretCollider = GetComponent<BoxCollider2D>();
        SetVariables();
        grabController.OnGrabTurret -= TurretHasBeenGrabbed;
        grabController.OnGrabTurret += TurretHasBeenGrabbed;
        _shootGapTime = new WaitForSeconds(shootGapTime);
        _cooldownTime = new WaitForSeconds(cooldownTime);
        _aimLockTime = new WaitForSeconds(aimLockTime);

        _StayAimLock = StayAimLock();
        _ShootMultipleBullets = ShootMultipleBullets();
        _WaitForCooldown = WaitForCooldown();
        _StayAiming = StayAiming();
        _aimTime = new WaitForSeconds(aimTime);
        StateMachine.Initialize(PopUpState);
    }

    public void DisableCollider()
    {
        turretCollider.enabled = false;
        onTurretDeath?.Invoke();
    }

    private void TurretHasBeenGrabbed()
    {
        if (grabController.GetGrabbedTurretInstanceId() == gameObject.GetInstanceID())
        {
            this.isTurretGrabbed = true;
        }

    }

    private void GrabOffTurret()
    {
        isTurretGrabbed = false;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();


    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void ChangeToAimingState()
    {
        if (!isTurretGrabbed)
        {
            StateMachine.ChangeState(AimingState);
        }
    }

    public void StartAiming()
    {
        StartCoroutine(_StayAiming); //왜 최적화 하니까 2번째에서부터는 인식을 못하지
        //StartCoroutine(StayAiming());
    }

    public void StopAiming()
    {
        StopCoroutine(_StayAiming);
        //StopCoroutine(StayAiming());
    }

    private IEnumerator StayAiming()
    {
        while (true)
        {
            yield return _aimTime;
            //StateMachine.ChangeState(ShootState);
            StateMachine.ChangeState(AimLockState);

            StopCoroutine(_StayAiming);
            yield return null;
        }
    }

    public void StartAimLock()
    {
        StartCoroutine(_StayAimLock);
    }

    public void StopAimLock()
    {
        StopCoroutine(_StayAimLock);
    }
    private IEnumerator StayAimLock()
    {
        while (true)
        {
            yield return _aimLockTime;
            StateMachine.ChangeState(ShootState);

            StopCoroutine(_StayAimLock);
            yield return null;
        }
    }


    private TurretBullet CreateBullet()
    {
        TurretBullet createdBullet = Instantiate(bulletPrefab);
        createdBullet.bulletPool = turretBulletPool;
        return createdBullet;
    }

    private TurretBrokenParts CreateBrokenParts()
    {
        TurretBrokenParts createdParts = Instantiate(brokenPartsPrefabs[UnityEngine.Random.Range(0, brokenPartsPrefabs.Length)]);
        createdParts.brokenPartsPool = brokenPartsObjectPool;
        return createdParts;
    }

    public void SpreadBrokenParts()
    {
        for (int i = 0; i < howManyBrokenParts; ++i)
        {
            TurretBrokenParts newParts = brokenPartsObjectPool.GetFromPool();
            newParts.transform.position = transform.position;
            newParts.SetRandomVelocity();
        }
    }


    public void ShootBulletWide() // to give angles some variety
    {
        TurretBullet newBullet = turretBulletPool.GetFromPool();
        randomShootAngle = UnityEngine.Random.Range(minRandomAngle, maxRandomAngle);
        newBullet.transform.position = gunController.GetGunTipPosition();
        rotation = gunController.GetGunTipRotation().eulerAngles;
        bulletRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z + randomShootAngle);
        newBullet.transform.rotation = bulletRotation;
        newBullet.SetVelocity();
    }


    public void StartShooting()
    {
        _ShootMultipleBullets = ShootMultipleBullets();
        StartCoroutine(_ShootMultipleBullets);
        //StartCoroutine(ShootMultipleBullets());
    }
    public void StopShooting()
    {
        shotBulletNumber = 0;
        //StopCoroutine(ShootMultipleBullets());
        StopCoroutine(_ShootMultipleBullets);
    }
    private IEnumerator ShootMultipleBullets()
    {
        while (shotBulletNumber <= shotBulletMaxNumber)
        {
            ShootBulletWide();
            ++shotBulletNumber;
            GameManager.Instance.audioManager.Play("turretShoot");
            yield return _shootGapTime;
        }
        shotBulletNumber = 0;
        StopCoroutine(_ShootMultipleBullets);

        if (!isTurretGrabbed)
        {
            OnFinishedShooting?.Invoke();
        }
    }


    private IEnumerator WaitForCooldown()
    {
        while (true)
        {
            yield return _cooldownTime;
            OnFinishedCooldown?.Invoke();
            StopCoroutine(_WaitForCooldown);
            yield return null;
        }

        //if (!isTurretGrabbed)
        //{
        //    OnFinishedCooldown?.Invoke();
        //}

    }

    public Vector3 ReturnPlayerHoldPosition()
    {
        return playerHoldPosition.position;
    }

    public void WaitUntilCooldown()
    {
        StartCoroutine(_WaitForCooldown);
        //StartCoroutine(WaitForCooldown());
    }

    public void StopCooldown()
    {
        StopCoroutine(_WaitForCooldown);
        //StopCoroutine(WaitForCooldown());
    }

    private void SetVariables()
    {
        aimTime = turretData.aimTime;
        minRandomAngle = turretData.minRandomAngle;
        maxRandomAngle = turretData.maxRandomAngle;
        shootGapTime = turretData.shootGapTime;
        shotBulletMaxNumber = turretData.shotBulletMaxNumber;
        cooldownTime = turretData.cooldownTime;
        aimLockTime = turretData.aimLockTime;
    }

    private void OnGetBulletFromPool(TurretBullet bullet) => bullet.gameObject.SetActive(true);
    private void OnReturnBulletToPool(TurretBullet bullet) => bullet.gameObject.SetActive(false);

    private void OnGetBrokenPartsFromPool(TurretBrokenParts parts) => parts.gameObject.SetActive(true);
    private void OnReturnBrokenPartsToPool(TurretBrokenParts parts) => parts.gameObject.SetActive(false);

    
    private void OnBecameVisible()
    {
        if (!this.isTurretGrabbed)
        {
            WarningInsideAnimator.enabled = true;
            WarningOutsideAnimator.enabled = false;
            WarningIconInsideRenderer.enabled = true;
            WarningIconOutsideRenderer.enabled = false;
        }
        
    }

    private void OnBecameInvisible()
    {
        if (!this.isTurretGrabbed)
        {
            WarningInsideAnimator.enabled = false;
            WarningOutsideAnimator.enabled = true;
            WarningIconInsideRenderer.enabled = false;
            WarningIconOutsideRenderer.enabled = true;
        }
        
    }

    public void TurnOffSprite()
    {
        WarningInsideAnimator.enabled = false;
        WarningOutsideAnimator.enabled = false;
        WarningIconInsideRenderer.enabled = false;
        WarningIconOutsideRenderer.enabled = false;
    }

}
