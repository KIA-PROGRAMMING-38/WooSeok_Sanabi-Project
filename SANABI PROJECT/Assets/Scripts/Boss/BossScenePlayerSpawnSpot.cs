using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BossScenePlayerSpawnSpot : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.bossScenePlayerSpawnSpot = transform.position;
        GameManager.Instance.hasSceneChanged = true;
    }
    void Start()
    {
        //GameManager.Instance.bossScenePlayerSpawnSpot.position = transform.position;
        //GameManager.Instance.hasSceneChanged = true;
    }

    void Update()
    {
        
    }
}
