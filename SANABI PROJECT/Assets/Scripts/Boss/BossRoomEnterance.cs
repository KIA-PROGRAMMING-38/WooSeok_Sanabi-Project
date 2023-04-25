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

    public GameObject bossPrefab;
    public Transform bossSpawnSpot;
    public BossGunController bossGunController;

    public Transform[] bossRunAwaySpots;

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
            enteranceCollider.enabled = true;
            enteranceCollider.isTrigger = false;
            boxCollider.enabled = false;
            //var bossObject = Instantiate(bossPrefab, bossSpawnSpot.position, bossSpawnSpot.rotation);
            var bossObject = Instantiate(bossPrefab, bossSpawnSpot.position, bossSpawnSpot.rotation);
            bossObject.GetComponent<BossController>().bossRunAwaySpots = bossRunAwaySpots;

            //Instantiate(bossPrefab, bossSpawnSpot);
            //GameManager.Instance.bossData = bossPrefab.GetComponent<BossData>();
            //GameManager.Instance.bossController = bossPrefab.GetComponent<BossController>();
            //GameManager.Instance.bossGunController = bossPrefab.GetComponentInChildren<BossGunController>();
            //GameManager.Instance.bossGunController.target = GameManager.Instance.playerController.transform;
        }
    }

}
