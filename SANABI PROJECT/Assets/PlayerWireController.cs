using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireController : MonoBehaviour
{
    LineRenderer lineRenderer;
    int normalWallLayerNumber;
    int hitNumber;
    RaycastHit2D[] _hits; // Array to store rayCast hits
    public RaycastHit2D _hitTarget;
    public Vector2 distanceVector;
    public float angle;
    Camera mainCam;
    PlayerData playerData;
    public bool IsGrappled { get; private set; }
    [SerializeField] private GrabController grabController;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        normalWallLayerNumber = LayerMask.NameToLayer("NormalWall");
        mainCam = Camera.main;
        playerData = GetComponentInParent<PlayerData>();
        _hits = new RaycastHit2D[2];
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
    }

    public void DeleteEnableLine()
    {
        lineRenderer.enabled = false;
    }
    



    public void FollowLine()
    {

    }
}
