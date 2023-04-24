using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingSceneController : MonoBehaviour
{
    public bool isFullScreen;
    public float volume = 1f;
    

    private void Awake()
    {
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.UnloadSceneAsync((int)GameManager.SceneNumber.Settings);
            GameManager.Instance.currentSceneNumber = GameManager.SceneNumber.Main;
            GameManager.Instance.ShowPauseCanvas(true);
        }
    }

    
}
