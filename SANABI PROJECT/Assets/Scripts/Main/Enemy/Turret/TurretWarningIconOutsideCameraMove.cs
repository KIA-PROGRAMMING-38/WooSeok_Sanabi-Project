using UnityEngine;

public class TurretWarningIconOutsideCameraMove : MonoBehaviour
{
    private Rect cameraBounds = new Rect(0, 0, 1, 1);
    private Camera mainCamera;
    [SerializeField] private Transform turretTransform;
    private Vector2 bottomLeft;
    private Vector2 topRight;
    private Vector3 clampedPosition;
    [SerializeField] private float gapBetweenCameraFrame = 0.5f;
    private Vector3 bottomLeftWorldPoint;
    private Vector3 topRightWorldPoint;

    void Start()
    {
        mainCamera = Camera.main;
        bottomLeftWorldPoint = new Vector3(0, 0, mainCamera.nearClipPlane);
        topRightWorldPoint = new Vector3(1, 1, mainCamera.nearClipPlane);
    }

    void Update()
    {
        bottomLeft = mainCamera.ViewportToWorldPoint(bottomLeftWorldPoint);
        topRight = mainCamera.ViewportToWorldPoint(topRightWorldPoint);

        cameraBounds = Rect.MinMaxRect(bottomLeft.x + gapBetweenCameraFrame, bottomLeft.y + gapBetweenCameraFrame, 
                                       topRight.x - gapBetweenCameraFrame, topRight.y - gapBetweenCameraFrame);


        clampedPosition.x = Mathf.Clamp(turretTransform.position.x, cameraBounds.xMin, cameraBounds.xMax);
        clampedPosition.y = Mathf.Clamp(turretTransform.position.y, cameraBounds.yMin, cameraBounds.yMax);

        transform.position = clampedPosition;
    }
}
