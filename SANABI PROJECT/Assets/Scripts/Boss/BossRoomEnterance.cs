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
    public Transform bossAppearSpot;

    [SerializeField] private float cameraWaitTime = 2f;
    private WaitForSeconds _cameraWaitTime;
    private IEnumerator _WaitForCameraAction;

    private void Awake()
    {
        playerLayerMask = LayerMask.NameToLayer("SNB");
        boxCollider= GetComponent<BoxCollider2D>();
        rigidbody = GetComponentInParent<Rigidbody2D>();

        _cameraWaitTime = new WaitForSeconds(cameraWaitTime);
        _WaitForCameraAction = WaitForCameraAction();
    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayerMask)
        {
            enteranceCollider.enabled = true;
            enteranceCollider.isTrigger = false;
            boxCollider.enabled = false;
            //var bossObject = Instantiate(bossPrefab, bossSpawnSpot.position, bossSpawnSpot.rotation);
            StartCoroutine(_WaitForCameraAction);
            GameManager.Instance.cameraFollow.StartFilmBossAppear(bossAppearSpot);
        }
    }

    private IEnumerator WaitForCameraAction()
    {
        yield return _cameraWaitTime;
        var bossObject = Instantiate(bossPrefab, bossSpawnSpot.position, bossSpawnSpot.rotation);
        bossObject.GetComponent<BossController>().bossRunAwaySpots = bossRunAwaySpots;
    }

}
