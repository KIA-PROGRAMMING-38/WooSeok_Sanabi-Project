using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireController : MonoBehaviour
{
    LineRenderer lineRenderer;
    PlayerData playerData;
    Camera mainCam;
    [SerializeField] private GrabController grabController;
    
    int normalWallLayerNumber;
    int hitNumber;
    public float angle;
    public bool IsGrappled { get; private set; }

    RaycastHit2D[] _hits; // Array to store rayCast hits
    public RaycastHit2D _hitTarget;
    public Vector2 distanceVector;

    private Color enableColor = new Color(0f, 255f, 240f);
    private Color linkedColor = Color.yellow;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mainCam = Camera.main;
        playerData = GetComponentInParent<PlayerData>();
        _hits = new RaycastHit2D[2];
        normalWallLayerNumber = LayerMask.NameToLayer("NormalWall");
    }

    void Update()
    {
        distanceVector = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;


        hitNumber = Physics2D.RaycastNonAlloc(transform.position, distanceVector.normalized, _hits, playerData.wireLength);
        IsGrappled = grabController.isGrappled;
        
        
        if (IsItHit() && !IsGrappled)
        {
            DrawEnableLine();
        }
        else if (IsGrappled)
        {
            DrawLinkedLine();
        }
        else
        {
            DeleteEnableLine();
        }
    }

    

    private bool IsItHit()
    {
        if (2 <= hitNumber)
        {
            _hitTarget = _hits[1];
            if (_hitTarget.collider.gameObject.layer == normalWallLayerNumber) // to exclude the SNB arm itself
            {
                return true;
            }
        }
        return false;
    }
    public void DrawEnableLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, _hitTarget.point);
        lineRenderer.startColor = enableColor;
        lineRenderer.endColor = enableColor;
    }
    
    public void DrawLinkedLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, grabController.HoldPosition);
        lineRenderer.startColor = linkedColor;
        lineRenderer.endColor = linkedColor;
    }

    public void DeleteEnableLine()
    {
        lineRenderer.enabled = false;
    }
    

}
