using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SwitchToTitleScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Title);
    }

    public void SwitchToSettingsScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Settings, LoadSceneMode.Additive);
    }

    public void TurnOffTheGame()
    {
        Application.Quit();
    }

    public void RestartMainScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Main);
        Time.timeScale = 1.0f;
    }
}
