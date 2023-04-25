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

    public GameObject bossPrefab;
    public Transform bossSpawnSpot;
    public BossGunController bossGunController; 

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
            Debug.Log("ÇÃ·¹ÀÌ¾î ºÎµúÈû!");
            enteranceCollider.isTrigger = false;
            boxCollider.enabled = false;
            hasPlayerEnteredRoom = true;
            Instantiate(bossPrefab, bossSpawnSpot);
            GameManager.Instance.bossData = bossPrefab.GetComponent<BossData>();
            GameManager.Instance.bossController = bossPrefab.GetComponent<BossController>();
            GameManager.Instance.bossGunController = bossPrefab.GetComponentInChildren<BossGunController>();
            GameManager.Instance.bossGunController.target = GameManager.Instance.playerController.transform;
        }
    }


    public bool HasPlayerEnteredRoom()
    {
        return hasPlayerEnteredRoom;
    }
}
