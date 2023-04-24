using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossEnterance : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObject timeSlower;
    [SerializeField] GameObject wireDashIcon;
    [SerializeField] GameObject hpRobot;

    [SerializeField] GameObject[] playerRelatedObjects;
    private bool isAllTurretsDead;
    void Start()
    {
        turretSpawner.OnAllTurretsDead -= CheckIfAllTurretsDead;
        turretSpawner.OnAllTurretsDead += CheckIfAllTurretsDead;
    }

    void Update()
    {
        Debug.Log(isAllTurretsDead);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAllTurretsDead)
        {
            SceneManager.LoadScene((int)GameManager.SceneNumber.Boss);
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
