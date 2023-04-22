using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public enum SceneNumber
    {
        Title,
        Settings,
        Main,
        Pause,
        Boss
    }

    
    public static GameManager Instance { get; private set; }
    public PlayerController playerController;
    public GrabController grabController;
    public PlayerArmController armController;
    public TurretSpawner turretSpawner;
    public PlayerData playerData;
    public WireDashIconController wireDashIconController;
    public SceneNumber currentSceneNumber;


    [SerializeField] private Canvas pauseCanvas;

    public bool isGamePaused;
    private void Awake()
    {
        Instance = this;
        currentSceneNumber = SceneNumber.Main;
    }

    private void Update()
    {
        if (currentSceneNumber == SceneNumber.Main)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseCanvas.gameObject.activeSelf) // if paused
                {
                    SetGameContinue();
                    //isGamePaused = false;
                    //pauseCanvas.gameObject.SetActive(false);
                    //Time.timeScale = 1f;
                }
                else // if not paused
                {
                    SetGamePause();
                    //isGamePaused = true;
                    //pauseCanvas.gameObject.SetActive(true);
                    //Time.timeScale = 0f;
                }
            }
        }
        
    }

    public void SetGamePause()
    {
        isGamePaused = true;
        //pauseCanvas.gameObject.SetActive(true);
        ShowPauseCanvas(isGamePaused);
        Time.timeScale = 0f;
    }

    public void SetGameContinue()
    {
        isGamePaused = false;
        //pauseCanvas.gameObject.SetActive(false);
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
