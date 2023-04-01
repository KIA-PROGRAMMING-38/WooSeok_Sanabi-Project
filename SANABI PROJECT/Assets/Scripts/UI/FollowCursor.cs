using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    private Camera mainCamera;
    private Vector2 CursorPoint;
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Confined; // Limit the position of cursur within the Game frame
    }

    void Update()
    {
        CursorPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = CursorPoint;
    }
}
