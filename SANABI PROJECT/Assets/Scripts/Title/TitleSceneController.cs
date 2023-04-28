using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    public TempCheck tempCheck;
    public TitleScreenShaker titleScreenShaker;
    private void Awake()
    {
        //gameManager = FindAnyObjectByType<GameManager>();
    }

    
    public void ChangeToMainScene()
    {
        //GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Main;
        //GameManager.Instance.lastSceneNumber= GameManager.SceneNumber.Title;
        SceneManager.LoadScene((int)GameManager.SceneNumber.Main);
        Time.timeScale = 1f;
        titleScreenShaker.StopShakeCameraCoroutine();
        //DontDestroyOnLoad(gameManager);
        
    }

    public void ChangeToSettingScene()
    {
        //GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Settings;
        //GameManager.Instance.lastSceneNumber = GameManager.SceneNumber.Title;
        SceneManager.LoadScene((int)GameManager.SceneNumber.Settings, LoadSceneMode.Additive);
        //DontDestroyOnLoad(gameManager);
        this.gameObject.SetActive(false);
        
    }

    public void TurnOffGame()
    {
        Application.Quit();
    }

}
