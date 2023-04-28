using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonScript : MonoBehaviour
{
    
    

    public void SwitchToSettingsScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Settings, LoadSceneMode.Additive);
        this.gameObject.SetActive(false);
        GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Settings;
    }

    public void SwitchToTitleScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Title);
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
