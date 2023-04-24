using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunController : MonoBehaviour
{
    public BossData bossData;


    public Transform target;



    private Vector2 targetDistance;
    private float rotationAngle;

    #region Coroutine Optimization

    private IEnumerator _LookAtTarget;

    private IEnumerator _AimAtTarget;
    private WaitForSeconds _aimWaitTime;

    #endregion

    #region Events

    public event Action OnFinishedAiming;

    #endregion
    void Start()
    {
        _LookAtTarget = LookAtTarget();

        _AimAtTarget = AimAtTarget();
        _aimWaitTime = new WaitForSeconds(bossData.aimWaitTime);
    }

    void Update()
    {
        
    }


    #region Coroutines

    #region LookingAtTarget
    public void StartLookingAtTarget()
    {
        StartCoroutine(_LookAtTarget);
    }

    public void StopLookingAtTarget()
    {
        StopCoroutine(_LookAtTarget);
    }

    private IEnumerator LookAtTarget()
    {
        while (true)
        {
            RotateHead();
            yield return null;
        }
    }
    private void RotateHead()
    {
        targetDistance = transform.position - target.position;
        rotationAngle = Mathf.Atan2(targetDistance.y, targetDistance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle + 90f);
    }

    #endregion

    #region AimAtTarget

    public void StartAimingAtTarget()
    {
        StartCoroutine(_AimAtTarget);
    }

    public void StopAimingAtTarget()
    {
        StopCoroutine(_AimAtTarget);
        StopCoroutine(_LookAtTarget);
    }

    private IEnumerator AimAtTarget()
    {
        yield return _aimWaitTime;
        OnFinishedAiming?.Invoke();
    }

    #endregion




    #endregion


}
