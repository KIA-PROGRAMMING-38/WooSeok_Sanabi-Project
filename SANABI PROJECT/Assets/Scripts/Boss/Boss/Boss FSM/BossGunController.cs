using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BossGunController : MonoBehaviour
{
    public BossData bossData;


    public Transform target;

    public Vector2 gunTipDistance { get; private set; }
    public LineRenderer lineRenderer;
    public Transform head;
    public Transform gunTip;


    private Color initialColor = Color.red;
    private Color yellowColor = Color.yellow;
    private Color whieColor = Color.white;
    [SerializeField] private float colorStayTime = 0.05f;
    private WaitForSeconds _colorStayTime;
    private IEnumerator _EnableLineColorChange;

    private Vector2 targetDistance;
    private float rotationAngle;

    #region Coroutine Optimization

    private IEnumerator _LookAtTarget;

    private IEnumerator _AimAtTarget;

    private IEnumerator _AimLockWait;
    private WaitForSeconds _aimLockTime;
    #endregion

    #region Events

    public event Action OnFinishedAiming;
    public event Action OnFinishedAimLock;

    #endregion
    void Start()
    {

        lineRenderer = GetComponentInChildren<LineRenderer>();

        _LookAtTarget = LookAtTarget();

        _AimAtTarget = AimAtTarget();

        _AimLockWait = AimLockWait();
        _aimLockTime = new WaitForSeconds(bossData.aimLockTime);

        _EnableLineColorChange = EnableLineColorChange();
        _colorStayTime = new WaitForSeconds(colorStayTime);
    }

    void Update()
    {
        
    }


    #region SetTarget

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    #endregion

    #region Laser Functions




    #region Aim Line Color Change 
    private void EnableLineInitialColor()
    {
        lineRenderer.startColor = initialColor;
        lineRenderer.endColor = initialColor;
    }
    private void EnableLineYellowColor()
    {
        lineRenderer.startColor = yellowColor;
        lineRenderer.endColor = yellowColor;
    }

    private void EnableLineWhiteColor()
    {
        lineRenderer.startColor = whieColor;
        lineRenderer.endColor = whieColor;
    }

    public void StartAimLineColorChange()
    {
        lineRenderer.enabled = true;
        _EnableLineColorChange = EnableLineColorChange();
        StartCoroutine(_EnableLineColorChange);
    }

    public void StopAimLineColorChange()
    {
        lineRenderer.enabled = false;
        EnableLineInitialColor();
        StopCoroutine(_EnableLineColorChange);
    }
    private IEnumerator EnableLineColorChange()
    {
        while (true)
        {
            EnableLineInitialColor();
            yield return _colorStayTime;
            EnableLineYellowColor();
            yield return _colorStayTime;
            EnableLineWhiteColor();
        }
    }

    #endregion

    #region Draw AimLine Towards Player
    public void DrawAimLineTowardsPlayer()
    {
        gunTipDistance = gunTip.position - head.transform.position;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, gunTipDistance * 1000f);
    }
    #endregion


    #endregion

    #region Coroutines

    #region LookingAtTarget
    public void StartLookingAtTarget()
    {
        _LookAtTarget = LookAtTarget();
        if (target != null)
        {
            StartCoroutine(_LookAtTarget);
        }
        //StartCoroutine(_LookAtTarget);
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rotationAngle + 90f), bossData.rotateSpeed * Time.deltaTime);

    }
    
    #endregion

    #region AimAtTarget

    public void StartAimingAtTarget()
    {
        lineRenderer.enabled = true;
        _AimAtTarget= AimAtTarget();
        StartCoroutine(_AimAtTarget);
    }

    public void StopAimingAtTarget()
    {
        StopCoroutine(_AimAtTarget);
    }

    private IEnumerator AimAtTarget()
    {
        float aimTime = bossData.aimWaitTime;
        while (0f <= aimTime)
        {
            aimTime -= Time.deltaTime;
            DrawAimLineTowardsPlayer();
            yield return null;
        }
        StopCoroutine(_AimAtTarget);
        OnFinishedAiming?.Invoke();
    }

    #endregion

    #region AimLockWait

    public void StartAimLock()
    {
        _AimLockWait = AimLockWait();
        StartCoroutine(_AimLockWait);
    }

    public void StopAimLock()
    {
        StopCoroutine(_AimLockWait);
    }

    private IEnumerator AimLockWait()
    {
        yield return _aimLockTime;
        OnFinishedAimLock?.Invoke();
    }

    #endregion


    #endregion


}
