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
    private float rotateSpeed;

    RaycastHit2D hit;
    IEnumerator _StartRotationAndAim;

    private Color initialColor = Color.red;
    private Color yellowColor = Color.yellow;
    private Color whieColor = Color.white;

    private IEnumerator _EnableLineColorChange;
    [SerializeField] private float colorStayTime = 0.05f;
    private WaitForSeconds _colorStayTime;
    private void Awake()
    {
        //aimLineRenderer = GetComponentInChildren<LineRenderer>();

    }
    private void OnEnable()
    {
        //targetTransform = GameManager.Instance.playerController.gameObject.transform;
        //aimLineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Start()
    {
        targetTransform = GameManager.Instance.playerController.gameObject.transform;
        aimLineRenderer = GetComponentInChildren<LineRenderer>();
        //gameObject.SetActive(true);
        platformLayerMask = (1 << LayerMask.NameToLayer("NormalWall")) | (1 << LayerMask.NameToLayer("NoGrabWall") | (1 << LayerMask.NameToLayer("Magma")));
        rotateSpeed = turretData.rotateSpeed;
        _StartRotationAndAim = StartRotationAndAim();
        _EnableLineColorChange = EnableLineColorChange();
        _colorStayTime = new WaitForSeconds(colorStayTime);
        //Gun.SetActive(false);
    }


    private void Update()
    {
        
    }

    private void EnableLineInitialColor()
    {
        aimLineRenderer.startColor = initialColor;
        aimLineRenderer.endColor = initialColor;
    }
    private void EnableLineYellowColor()
    {
        aimLineRenderer.startColor = yellowColor;
        aimLineRenderer.endColor = yellowColor;
    }
    
    private void EnableLineWhiteColor()
    {
        aimLineRenderer.startColor = whieColor;
        aimLineRenderer.endColor = whieColor;
    }

    public void StartColorChange()
    {
        aimLineRenderer.enabled = true;
        StartCoroutine(_EnableLineColorChange);
    }

    public void StopColorChange()
    {
        aimLineRenderer.enabled = false;
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

    

    public void TryStartRotationAndAim()
    {
        aimLineRenderer.enabled = true;
        StartCoroutine(_StartRotationAndAim);
    }

    public void StopRotationAndAim()
    {
        aimLineRenderer.enabled = false;
        StopCoroutine(_StartRotationAndAim);
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
