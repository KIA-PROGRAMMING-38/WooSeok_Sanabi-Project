using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{

    public void ChangeToMainScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Main);
    }

    public void ChangeToSettingScene()
    {
        SceneManager.LoadScene((int)GameManager.SceneNumber.Settings, LoadSceneMode.Additive);
    }

    public void TurnOffGame()
    {
        Application.Quit();
    }
}
