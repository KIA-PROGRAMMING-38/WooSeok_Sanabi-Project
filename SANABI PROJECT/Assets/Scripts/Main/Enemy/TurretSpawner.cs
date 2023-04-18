using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] turretSpawnSpots;
    [SerializeField] private int spawnNumber = 3;
    [SerializeField] GameObject turretPrefab;
    private TurretController turretController;
    private bool[] alreadySpawnedSpots;
    private bool isPhase1;
    private int deadTurretNumber;
    private bool isPhase2;

    [SerializeField] private float phaseChangeWaitTime = 5f;
    private WaitForSeconds _phaseChangeWaitTime;
    private IEnumerator _WaitForPhaseChange;
    void Start()
    {
        alreadySpawnedSpots = new bool[turretSpawnSpots.Length];
        _phaseChangeWaitTime = new WaitForSeconds(phaseChangeWaitTime);
        _WaitForPhaseChange = WaitForPhaseChange();
    }

    void Update()
    {
        if (spawnNumber <= deadTurretNumber && isPhase1 && !isPhase2)
        {
            isPhase1 = false;
            //StartWaitPhaseChange();  // second turrets get spawned defected.... :(
            SpawnTurrets();
        }
    }

    private void StartWaitPhaseChange()
    {
        StartCoroutine(_WaitForPhaseChange);
    }

    private void StopWaitPhaseChange()
    {
        StopCoroutine(_WaitForPhaseChange);
    }

    private IEnumerator WaitForPhaseChange()
    {
        while (true)
        {
            yield return _phaseChangeWaitTime;
            EnterPhase2();
            yield return null;
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPhase1)
            {
                EnterPhase1();
            }
            
            //SpawnTurrets();
            //SpawnAllTurrets();
        }
    }

    private void EnterPhase1()
    {
        isPhase1 = true;
        SpawnTurrets();
    }

    private void EnterPhase2()
    {
        isPhase2 = true;
        SpawnTurrets();
    }

    private void SpawnTurrets()
    {
        int turretCount = 0;
        int tryCount = 0;
        while (turretCount < spawnNumber)
        {
            ++tryCount;
            int index = UnityEngine.Random.Range(0, turretSpawnSpots.Length);
            if (alreadySpawnedSpots[index] == false)
            {
                ++turretCount;
                alreadySpawnedSpots[index] = true;
                GameObject newTurret = Instantiate(turretPrefab, turretSpawnSpots[index].position, turretSpawnSpots[index].rotation);
            }
            if (turretSpawnSpots.Length * 5 <= tryCount)
            {
                break;
            }
        }
    }

    private void SpawnAllTurrets()
    {
        for (int i = 0; i < turretSpawnSpots.Length; ++i)
        {
            GameObject newTurret = Instantiate(turretPrefab, turretSpawnSpots[i].position, turretSpawnSpots[i].rotation);
        }
    }


    public void AddTurretDeathCount()
    {
        ++deadTurretNumber;
    }

}
