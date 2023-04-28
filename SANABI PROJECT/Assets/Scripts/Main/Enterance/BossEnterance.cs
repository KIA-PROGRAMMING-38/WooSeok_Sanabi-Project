using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnterance : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    //[SerializeField] GameObject gameManager;
    //[SerializeField] GameObject player;
    //[SerializeField] GameObject timeSlower;
    //[SerializeField] GameObject wireDashIcon;
    //[SerializeField] GameObject hpRobot;

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
