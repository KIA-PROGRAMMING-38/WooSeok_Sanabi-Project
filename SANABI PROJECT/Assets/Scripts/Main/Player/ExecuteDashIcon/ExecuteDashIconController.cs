using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteDashIconController : MonoBehaviour
{
    private Vector2 mouseDirection;
    private float rotationAngle;
    private SpriteRenderer spriteRenderer;
    private IEnumerator _FollowCursor;
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        _FollowCursor = FollowCursor();
    }

    void Update()
    {
        //mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //rotationAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle -90f);
    }


    public void StartFollowingCursor()
    {
        spriteRenderer.enabled = true;
        StartCoroutine(_FollowCursor);
    }

    public void StopFollowingCursor()
    {
        spriteRenderer.enabled = false;
        StopCoroutine(_FollowCursor);
    }

    private IEnumerator FollowCursor()
    {
        while (true)
        {
            mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            rotationAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle - 90f);
            yield return null;
        }
    }
}
