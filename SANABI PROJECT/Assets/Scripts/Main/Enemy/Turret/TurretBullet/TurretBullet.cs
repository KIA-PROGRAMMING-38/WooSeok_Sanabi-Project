using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public Rigidbody2D bulletRigidbody { private get; set; }
    public ObjectPool<TurretBullet> bulletPool { private get; set; }

    [SerializeField] private float bulletVelocity = 15f;

    private void Awake()
    {
        bulletRigidbody= GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetVelocity();
    }

    private void Update()
    {
        
    }

    public void SetVelocity()
    {
        bulletRigidbody.velocity = transform.up * bulletVelocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Player"))
        {
            bulletPool.ReturnToPool(this);
        }
    }
}
