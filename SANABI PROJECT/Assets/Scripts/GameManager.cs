using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //public static Color idleColor = Color.white;
    //public static Color hoverColor = new Color(255f, 0f, 178f); // «÷ «Œ≈©
    //public static float shakeIntensity = 0.05f;
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


    [SerializeField] private Canvas pauseCanvas;

    public bool isGamePaused;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.gameObject.activeSelf)
            {
                isGamePaused = false;
                pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                isGamePaused = true;
                pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }



}
