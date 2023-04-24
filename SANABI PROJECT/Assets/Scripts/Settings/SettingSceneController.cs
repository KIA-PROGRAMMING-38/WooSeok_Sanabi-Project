using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class SettingSceneController : MonoBehaviour
{
    private GameManager gameManager;
    public bool isFullScreen;
    public float volume = 1f;
    

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        //Instantiate(GameManager.Instance.SettingsCanvas);
    }
    private void Start()
    {
        //Instantiate(GameManager.Instance.SettingsCanvas);
    }

    private void OnEnable()
    {
        //Instantiate(GameManager.Instance.SettingsCanvas);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    SceneManager.UnloadSceneAsync((int)GameManager.SceneNumber.Settings);
        //    GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Main;
        //    GameManager.Instance.ShowPauseCanvas(true);
        //}

    }

    
}
