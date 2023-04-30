using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    private float slowIntensity;
    private WaitForSeconds slowTime;

    private IEnumerator _SlowDown;
    private void Start()
    {
        _SlowDown = SlowDown();
    }

    public void StopSlowDown()
    {
        Time.timeScale = 1f;
    }

    public void PleaseSlowDown(float slowIntensity, float slowTime)
    {
        ActivateSlowDown(slowIntensity, slowTime);
    }

    private void ActivateSlowDown(float howslow, float howlong)
    {
        slowIntensity = howslow;
        slowTime = new WaitForSeconds(howlong);
        _SlowDown = SlowDown();
        StartCoroutine(_SlowDown);
    }


    private IEnumerator SlowDown()
    {
        Time.timeScale = slowIntensity;
        yield return slowTime;
        Time.timeScale = 1f;
    }
}
