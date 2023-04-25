using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossRoomEnterance : MonoBehaviour
{
    private LayerMask playerLayerMask;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    public BoxCollider2D enteranceCollider;
    private bool hasPlayerEnteredRoom;

    private void Awake()
    {
        playerLayerMask = LayerMask.NameToLayer("SNB");
        boxCollider= GetComponent<BoxCollider2D>();
        rigidbody = GetComponentInParent<Rigidbody2D>();
    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerMask)
        {
            Debug.Log("�÷��̾� �ε���!");
            enteranceCollider.isTrigger = false;
            boxCollider.enabled = false;
            hasPlayerEnteredRoom = true;
        }
    }


    public bool HasPlayerEnteredRoom()
    {
        return hasPlayerEnteredRoom;
    }
}
