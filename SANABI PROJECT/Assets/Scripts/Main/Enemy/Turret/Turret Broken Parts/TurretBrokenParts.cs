using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBrokenParts : MonoBehaviour
{
    [SerializeField] private float explodeVelocity = 18f;
    public ObjectPool<TurretBrokenParts> brokenPartsPool { private get; set; }
    [SerializeField] private Rigidbody2D rigidbody;

    [SerializeField] private float lastTime = 6f;
    private WaitForSeconds _lastTime;
    private IEnumerator _WaitUntilGone;
    private Vector2 randomVelocity;
    void Start()
    {
        //rigidbody= GetComponent<Rigidbody2D>();
        _lastTime = new WaitForSeconds(lastTime);
        _WaitUntilGone = WaitUntilGone();
        StartVanishingTimer();
        SetRandomVelocity();
    }


    public void SetRandomVelocity()
    {
        randomVelocity.Set(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randomVelocity = randomVelocity.normalized * explodeVelocity;
        rigidbody.velocity = randomVelocity;
    }

    private void StartVanishingTimer()
    {
        StartCoroutine(_WaitUntilGone);
    }

    private void StopVanishingTimer()
    {
        StopCoroutine(_WaitUntilGone);
    }

    private IEnumerator WaitUntilGone()
    {
        while (true)
        {
            yield return _lastTime;
            brokenPartsPool.ReturnToPool(this);
            StopCoroutine(_WaitUntilGone);
            yield return null;
        }
        
    }

}
