using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public BossController bossController;
    public BossGunController bossGunController;
    public Rigidbody2D bulletRigid;
    public TrailRenderer trailRenderer;
    public CircleCollider2D circleCollider;

    private Vector2 shootDirection;
    [SerializeField] private float shootSpeed = 300f;

    private int platformLayer;
    private int playerLayer;

    private IEnumerator _WaitBullet;
    [SerializeField] private float bulletReturnTime = 1f;
    private WaitForSeconds _bulletReturnTime;

    private void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        platformLayer = LayerMask.NameToLayer("NormalWall");
        playerLayer = LayerMask.NameToLayer("SNB");
        circleCollider= GetComponent<CircleCollider2D>();   
    }
    private void Start()
    {
        bossController.OnShoot -= ShootBullet;
        bossController.OnShoot += ShootBullet;

        _WaitBullet = WaitBullet();
        _bulletReturnTime = new WaitForSeconds(bulletReturnTime);
        trailRenderer.emitting = false;
    }



    private void ShootBullet()
    {
        ReturnToHead();
        trailRenderer.emitting = true;
        shootDirection = bossGunController.gunTipDistance.normalized;
        bulletRigid.velocity = shootDirection * shootSpeed;
        circleCollider.enabled = true;
    }

    private void ReturnToHead()
    {
        trailRenderer.emitting = false;
        bulletRigid.velocity = Vector2.zero;
        transform.position = bossGunController.transform.position;
        StartWaitBullet();
    }

    private void StartWaitBullet()
    {
        _WaitBullet = WaitBullet();
        StartCoroutine(_WaitBullet);
    }

    private void StopWaitBullet()
    {
        StopCoroutine(_WaitBullet);
    }

    private IEnumerator WaitBullet()
    {
        circleCollider.enabled = false;
        yield return _bulletReturnTime;
        ReturnToHead();
        StopWaitBullet();
    }


}
