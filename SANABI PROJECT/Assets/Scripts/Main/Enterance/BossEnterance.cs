using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnterance : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private SpriteRenderer portalRenderer;
    private Color openColor = new Color(23f, 191f, 170f, 31f);
    public event Action OnBossEnterance;

    [SerializeField] GameObject[] playerRelatedObjects;
    private bool isAllTurretsDead;
    void Start()
    {
        turretSpawner.OnAllTurretsDead -= ConfirmAllTurretsDead;
        turretSpawner.OnAllTurretsDead += ConfirmAllTurretsDead;
        GameManager.Instance.audioManager.GradualVolumePlay("MainBGM");
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

        if (collision.gameObject.CompareTag("Player") && isAllTurretsDead) 
        {
            SceneManager.LoadScene((int)GameManager.SceneNumber.Boss);
            GameManager.Instance.lastSceneNumber = GameManager.SceneNumber.Main;
            GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Boss;
            GameManager.Instance.playerController.ClearAfterImagePool();
            GameManager.Instance.playerController.ClearDustPool();
            DontDestroyOnLoad(GameManager.Instance);
            GameManager.Instance.audioManager.Stop("MainBGM");
            OnBossEnterance?.Invoke();
            foreach (GameObject element in playerRelatedObjects)
            {
                DontDestroyOnLoad(element);
            }

        }
    }

    private void ConfirmAllTurretsDead()
    {
        isAllTurretsDead = true;
        portalRenderer.material.color = openColor;
    }
}
