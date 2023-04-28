using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public enum SceneNumber
    {
        Title,
        Settings,
        Main,
        Boss
    }


    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }

    }


    [Header("Player")]
    public PlayerController playerController;
    public GrabController grabController;
    public PlayerArmController armController;
    public TurretSpawner turretSpawner;
    public PlayerData playerData;
    public WireDashIconController wireDashIconController;
    public GameObject playerPrefab;
    public CameraFollow cameraFollow;
    public BossEnterance bossEnterance;

    private SceneNumber initialSceneNumber;
    public SceneNumber currentSceneNumber;
    public SceneNumber lastSceneNumber;
    public float ScreenShakeIntensity { get; set; }
    public bool isGamePaused;

    [Header("Boss")]
    public Transform playerSpawnSpot;
    public BossData bossData;
    public BossController bossController;
    public BossGunController bossGunController;
    public Transform playerGrabBossPos;
    public BossCanvasController bossCanvasController;

    public Vector3 bossScenePlayerSpawnSpot
    {
        set
        {
            playerPrefab.transform.position = value;
        }
    }

    [Header("Canvas")]
    //[SerializeField] public Canvas TitleCanvas;
    [SerializeField] public Canvas pauseCanvas;

    public bool hasSceneChanged;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        //else
        //{
        //    Destroy(this.gameObject);
        //}
        //DontDestroyOnLoad(this);

        initialSceneNumber = SceneNumber.Main;
        ScreenShakeIntensity = 1f;
        currentSceneNumber = initialSceneNumber;

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        

        if (currentSceneNumber == SceneNumber.Settings)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                lastSceneNumber = SceneNumber.Settings;
                currentSceneNumber = SceneNumber.Main;
                SceneManager.UnloadSceneAsync((int)lastSceneNumber);
                //TitleCanvas.gameObject.SetActive(true);
                Time.timeScale = 1f;
                isGamePaused= false;
            }
        }

        if (currentSceneNumber == SceneNumber.Main)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseCanvas.gameObject.activeSelf) // if paused
                {
                    SetGameContinue();
                }
                else // if not paused
                {
                    SetGamePause();
                }
            }
        }

        
        
    }

    public void SetGamePause()
    {
        isGamePaused = true;
        ShowPauseCanvas(isGamePaused);
        Time.timeScale = 0f;
    }

    public void SetGameContinue()
    {
        isGamePaused = false;
        ShowPauseCanvas(isGamePaused);
        Time.timeScale = 1f;
    }
    
    public void ShowPauseCanvas(bool toshowcanvas)
    {
        if (toshowcanvas)
        {
            pauseCanvas.gameObject.SetActive(true);
        }
        else
        {
            pauseCanvas.gameObject.SetActive(false);
        }
    }
}
