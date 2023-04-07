using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWireController : MonoBehaviour
{
    LineRenderer lineRenderer;
    int normalWallLayerNumber;
    int hitNumber;
    RaycastHit2D[] _hits; // Array to store rayCast hits
    Vector2 distanceVector; // 
    Camera mainCam;
    PlayerData playerData;
    public bool IsGrappled { get; private set; }
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
        hitNumber = Physics2D.RaycastNonAlloc(transform.position, distanceVector.normalized, _hits, playerData.wireLength);
        
        
        if (IsItHit())
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
            if (_hits[1].collider.gameObject.layer == normalWallLayerNumber) // to exclude the SNB arm itself
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
        lineRenderer.SetPosition(1, _hits[1].point);
        //lineRenderer.startColor = Color.blue;
        //lineRenderer.endColor = Color.blue;
    }

    public void DeleteEnableLine()
    {
        lineRenderer.enabled = false;
    }
    private bool isGrappled()
    {
        IsGrappled = false;


        return IsGrappled;
    }



    public void FollowLine()
    {

    }
}
