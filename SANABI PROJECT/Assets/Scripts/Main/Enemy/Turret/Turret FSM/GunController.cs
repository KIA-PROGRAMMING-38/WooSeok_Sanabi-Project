using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform gunTipTransform;
    [SerializeField] private TurretData turretData;
    private int platformLayerMask;
    private LineRenderer aimLineRenderer;
    private Vector2 targetDistance;
    private Vector2 gunTipDistance;
    private float rotateAngle;
    //[SerializeField] float rotateSpeed = 150f;
    private float rotateSpeed;

    RaycastHit2D hit;


    IEnumerator _startRotationAndAim;
    private void Awake()
    {
        aimLineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        platformLayerMask = (1 << LayerMask.NameToLayer("NormalWall")) | (1 << LayerMask.NameToLayer("NoGrabWall") | (1 << LayerMask.NameToLayer("Magma")));
        Gun.SetActive(false);
        rotateSpeed = turretData.rotateSpeed;
        _startRotationAndAim = StartRotationAndAim();
    }


    private void Update()
    {
        
    }

    public void TryStartRotationAndAim()
    {
        aimLineRenderer.enabled = true;
        StartCoroutine(_startRotationAndAim);
    }

    public void StopRotationAndAim()
    {
        aimLineRenderer.enabled = false;
        StopCoroutine(_startRotationAndAim);
    }
    
    IEnumerator StartRotationAndAim()
    {
        while (true)
        {
            RotateTowardsPlayer();
            DrawAimLineTowardsPlayer();
            yield return null;
        }
    }




    public void DrawAimLineTowardsPlayer()
    {
        gunTipDistance = gunTipTransform.position - Gun.transform.position;
        hit = Physics2D.Raycast(transform.position, gunTipDistance.normalized, 1000f, platformLayerMask);
        aimLineRenderer.SetPosition(0, gunTipTransform.position);

        if (hit.collider != null)
        {
            aimLineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            aimLineRenderer.SetPosition(1, gunTipDistance * 1000f);
        }
    }



    public void RotateTowardsPlayer()
    {
        targetDistance = targetTransform.position - Gun.transform.position;
        rotateAngle = Mathf.Atan2(targetDistance.y, targetDistance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rotateAngle - 90f), rotateSpeed * Time.deltaTime);
    }

    public void ShowGun()
    {
        Gun.SetActive(true);
    }

    public Vector2 GetGunTipPosition()
    {
        return gunTipTransform.position;
    }

    public Quaternion GetGunTipRotation()
    {
        return gunTipTransform.rotation;
    }
}
