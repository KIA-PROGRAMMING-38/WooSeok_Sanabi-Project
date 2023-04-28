using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossBullet : MonoBehaviour
{
    public BossController bossController;
    public BossGunController bossGunController;
    public Rigidbody2D bulletRigid;
    public TrailRenderer trailRenderer;
    public CircleCollider2D circleCollider;

    private Vector2 shootDirection;
    [SerializeField] private float shootSpeed = 300f;

    //private int platformLayer;
    //private int playerLayer;

    private IEnumerator _WaitBullet;
    [SerializeField] private float bulletReturnTime = 1f;
    //private WaitForSeconds _bulletReturnTime;

    private void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        //platformLayer = LayerMask.NameToLayer("NormalWall");
        //playerLayer = LayerMask.NameToLayer("SNB");
        circleCollider= GetComponent<CircleCollider2D>();   
    }
    private void Start()
    {
        bossController.OnShoot -= ShootBullet;
        bossController.OnShoot += ShootBullet;
        bossController.OnFinalBeamShoot -= ShootFinalBullet;
        bossController.OnFinalBeamShoot += ShootFinalBullet;

        //_WaitBullet = WaitBullet();
        //_bulletReturnTime = new WaitForSeconds(bulletReturnTime);
        trailRenderer.emitting = false;
        circleCollider.enabled = false;
    }

    public void ShootFinalBullet()
    {
        transform.position = bossGunController.transform.position;
        trailRenderer.emitting = true;
        Vector2 upDirection = default;
        upDirection.Set((GameManager.Instance.playerController.transform.position - bossGunController.transform.position).normalized.x, 1f);
        bulletRigid.velocity = upDirection * shootSpeed;

    }

    private void ShootBullet()
    {
        //ReturnToHead();
        trailRenderer.enabled = true;
        trailRenderer.emitting = true;
        shootDirection = bossGunController.gunTipDistance.normalized;
        bulletRigid.velocity = shootDirection * shootSpeed;
        circleCollider.enabled = true;
    }

    private void ReturnToHead()
    {
        circleCollider.enabled = false;
        trailRenderer.enabled = false;
        //trailRenderer.emitting = false;
        bulletRigid.velocity = Vector2.zero;
        transform.position = bossGunController.transform.position;
        //StartWaitBullet();
    }

    //private void StartWaitBullet()
    //{
    //    _WaitBullet = WaitBullet();
    //    StartCoroutine(_WaitBullet);
    //}

    //private void StopWaitBullet()
    //{
    //    StopCoroutine(_WaitBullet);
    //}

    //private IEnumerator WaitBullet()
    //{
    //    circleCollider.enabled = false;
    //    yield return _bulletReturnTime;
    //    ReturnToHead();
    //    StopWaitBullet();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletDeadZone"))
        {
            ReturnToHead();
        }
    }

}
