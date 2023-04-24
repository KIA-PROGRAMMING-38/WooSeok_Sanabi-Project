using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public BossController bossController;
    public BossGunController bossGunController;
    public Rigidbody2D bulletRigid;
    public TrailRenderer trailRenderer;

    private Vector2 shootDirection;
    [SerializeField] private float shootSpeed = 150f;

    private int platformLayer;
    private int playerLayer;

    private IEnumerator _WaitBullet;
    [SerializeField] private float bulletReturnTime = 2f;
    private WaitForSeconds _bulletReturnTime;

    private void Awake()
    {
        bulletRigid = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        platformLayer = LayerMask.NameToLayer("NormalWall");
        playerLayer = LayerMask.NameToLayer("SNB");
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
    }

    private void ReturnToHead()
    {
        bulletRigid.velocity = Vector2.zero;
        trailRenderer.emitting = false;
        transform.position = bossGunController.transform.position;
        StartWaitBullet();
    }

    private void StartWaitBullet()
    {
        StartCoroutine(_WaitBullet);
    }

    private void StopWaitBullet()
    {
        StopCoroutine(_WaitBullet);
    }

    private IEnumerator WaitBullet()
    {
        yield return _bulletReturnTime;
        ReturnToHead();
        StopWaitBullet();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            Debug.Log("플레이어가 맞았다!");
        }

        
    }
}
