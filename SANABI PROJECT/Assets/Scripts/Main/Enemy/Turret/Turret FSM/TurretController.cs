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
    public TurretShootState ShootState { get; private set; }
    public TurretExecuteHoldedState ExecuteHoldedState { get; private set; }
    public TurretDeadState DeadState { get; private set; }

    #endregion
    public Animator Animator { get; private set; }

    public GunController gunController;

    public IEnumerator _StayAiming;
    private WaitForSeconds _aimTime;
    [SerializeField] private float aimTime = 1.5f;
    public ObjectPool<TurretBullet> turretBulletPool { get; private set; }
    public TurretBullet bulletPrefab;

    [SerializeField][Range(5f, 10f)] private float minRandomAngle = 5f;
    [SerializeField][Range(10f, 20f)] private float maxRandomAngle = 10f;
    [SerializeField][Range(0.01f, 0.1f)] private float shootGapTime = 0.05f;
    private float randomShootAngle;
    private Vector3 rotation;
    private Quaternion bulletRotation;
    private WaitForSeconds _shootGapTime;
    private IEnumerator _ShootMultipleBullets;

    private int shotBulletNumber;
    [SerializeField] private int shotBulletMaxNumber = 15;
    public event Action OnFinishedShooting;

    private WaitForSeconds _cooldownTime;
    [SerializeField] private float cooldownTime = 0.5f;
    private IEnumerator _WaitForCooldown;

    public event Action OnFinishedCooldown;
    private void Awake()
    {
        StateMachine = new TurretStateMachine();
        turretData = GetComponentInParent<TurretData>();
        Animator = GetComponent<Animator>();


        PopUpState = new TurretPopUpState(this, StateMachine, turretData, "popUp");
        CooldownState = new TurretCooldownState(this, StateMachine, turretData, "coolDown");
        AimingState = new TurretAimingState(this, StateMachine, turretData, "aiming");
        ShootState = new TurretShootState(this, StateMachine, turretData, "shoot");
        ExecuteHoldedState = new TurretExecuteHoldedState(this, StateMachine, turretData, "executeHolded");
        DeadState = new TurretDeadState(this, StateMachine, turretData, "dead");

    }

    private void Start()
    {
        turretBulletPool = new ObjectPool<TurretBullet>(CreateBullet, OnGetBulletFromPool, OnReturnBulletToPool);
        _shootGapTime = new WaitForSeconds(shootGapTime);
        _cooldownTime = new WaitForSeconds(cooldownTime);

        _ShootMultipleBullets = ShootMultipleBullets();
        _WaitForCooldown = WaitForCooldown();
        _StayAiming = StayAiming();
        _aimTime = new WaitForSeconds(aimTime);
        StateMachine.Initialize(PopUpState);
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
        StateMachine.ChangeState(AimingState);
        gunController.ShowGun();
    }

    public void StartAiming()
    {
        //StartCoroutine(_StayAiming); 왜 최적화 하니까 2번째에서부터는 인식을 못하지
        StartCoroutine(StayAiming());
    }

    public void StopAiming()
    {
        //StopCoroutine(_StayAiming);
        StopCoroutine(StayAiming());
    }

    private IEnumerator StayAiming()
    {
        yield return _aimTime;
        StateMachine.ChangeState(ShootState);
    }

    private TurretBullet CreateBullet()
    {
        TurretBullet createdBullet = Instantiate(bulletPrefab);
        createdBullet.bulletPool = turretBulletPool;
        return createdBullet;
    }



    //public void ShootBullet()
    //{
    //    TurretBullet newBullet = turretBulletPool.GetFromPool();
    //    newBullet.transform.position = gunController.GetGunTipPosition();
    //    newBullet.transform.rotation = gunController.GetGunTipRotation();
    //    newBullet.SetVelocity();
    //}


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


    private IEnumerator ShootMultipleBullets()
    {
        while (shotBulletNumber <= shotBulletMaxNumber)
        {
            ShootBulletWide();
            ++shotBulletNumber;
            yield return _shootGapTime;
        }
        shotBulletNumber = 0;

        
        OnFinishedShooting?.Invoke();
    }

    public void StartShooting()
    {
        //StartCoroutine(_ShootMultipleBullets);
        StartCoroutine(ShootMultipleBullets());
    }

    private IEnumerator WaitForCooldown()
    {
        yield return _cooldownTime;
        OnFinishedCooldown?.Invoke();
    }

    public void WaitUntilCooldown()
    {
        //StartCoroutine(_WaitForCooldown);
        StartCoroutine(WaitForCooldown());
    }

    public void StopCooldown()
    {
        //StopCoroutine(_WaitForCooldown);
        StopCoroutine(WaitForCooldown());
    }

    private void OnGetBulletFromPool(TurretBullet bullet) => bullet.gameObject.SetActive(true);
    private void OnReturnBulletToPool(TurretBullet bullet) => bullet.gameObject.SetActive(false);
}
