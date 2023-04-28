using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnterance : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    public event Action OnBossEnterance;

    [SerializeField] GameObject[] playerRelatedObjects;
    private bool isAllTurretsDead;
    void Start()
    {
        turretSpawner.OnAllTurretsDead -= CheckIfAllTurretsDead;
        turretSpawner.OnAllTurretsDead += CheckIfAllTurretsDead;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player") && isAllTurretsDead)
        //{
        //    SceneManager.LoadScene((int)GameManager.SceneNumber.Boss);
        //    GameManager.Instance.lastSceneNumber = GameManager.SceneNumber.Main;
        //    GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Boss;
        //    GameManager.Instance.playerController.ClearAfterImagePool();
        //    GameManager.Instance.playerController.ClearDustPool();
        //    OnBossEnterance?.Invoke();
        //    foreach (GameObject element in playerRelatedObjects)
        //    {
        //        DontDestroyOnLoad(element);
        //    }
        //}

        if (collision.gameObject.CompareTag("Player")) // test¿ë = ÅÍ·¿µµ ´Ù Á×¾î¾ßÇÔ
        {
            SceneManager.LoadScene((int)GameManager.SceneNumber.Boss);
            GameManager.Instance.lastSceneNumber = GameManager.SceneNumber.Main;
            GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Boss;
            GameManager.Instance.playerController.ClearAfterImagePool();
            GameManager.Instance.playerController.ClearDustPool();
            DontDestroyOnLoad(GameManager.Instance);
            OnBossEnterance?.Invoke();
            foreach (GameObject element in playerRelatedObjects)
            {
                DontDestroyOnLoad(element);
            }

        }
    }

    private void CheckIfAllTurretsDead()
    {
        isAllTurretsDead = true;
    }
}
