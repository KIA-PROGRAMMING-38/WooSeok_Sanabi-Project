using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenChange : MonoBehaviour
{
    [SerializeField] private TMP_Text fullscreenText;
    private SettingSceneController settingController;
    //private Button[] buttons;

    private int fullScreenOn = 1;
    private int fullScreenOff = -1;

    private bool isFullScreen = true;
    public bool IsFullScreen { get; private set; }

    private void Awake()
    {
        //InitFullscreen();
        settingController = GetComponentInParent<SettingSceneController>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("isFullScreen"))
        {
            LoadFullScreenState();
        }
        else
        {
            InitFullscreen();
        }
    }

    private void OnEnable()
    {
        //isFullScreen = settingController.isFullScreen;
        //if (isFullScreen)
        //{
        //    fullscreenText.text = "ÄÑÁü";
        //}
        //else
        //{
        //    fullscreenText.text = "²¨Áü";
        //}
    }

    private void InitFullscreen()
    {
        PlayerPrefs.SetInt("isFullScreen", fullScreenOn);
        Screen.fullScreen = true;
        fullscreenText.text = "ÄÑÁü";
        //isFullScreen = true;
        //IsFullScreen = isFullScreen;
        //Screen.fullScreen = IsFullScreen;
        //fullscreenText.text = "ÄÑÁü";
    }

    private void LoadFullScreenState()
    {
        int fullScreenValue = PlayerPrefs.GetInt("isFullScreen");
        if (fullScreenValue == fullScreenOn)
        {
            Screen.fullScreen = true;
            fullscreenText.text = "ÄÑÁü";
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenText.text = "²¨Áü";
        }
    }


    public void OnClick()
    {
        if (PlayerPrefs.GetInt("isFullScreen") == fullScreenOn)
        {
            PlayerPrefs.SetInt("isFullScreen", fullScreenOff);
            Screen.fullScreen = false;
            fullscreenText.text = "²¨Áü";
        }
        else // if not full screen but clicked the button?
        {
            PlayerPrefs.SetInt("isFullScreen", fullScreenOn);
            Screen.fullScreen = true;
            fullscreenText.text = "ÄÑÁü";
        }


        //if (isFullScreen == true)
        //{
        //    isFullScreen = false;
        //    IsFullScreen = isFullScreen;
        //    Screen.fullScreen = IsFullScreen;
        //    fullscreenText.text = "²¨Áü";
        //    settingController.isFullScreen = IsFullScreen;
        //    Debug.Log(settingController.isFullScreen);
        //}
        //else
        //{
        //    isFullScreen = true;
        //    IsFullScreen = isFullScreen;
        //    Screen.fullScreen = IsFullScreen;
        //    fullscreenText.text = "ÄÑÁü";
        //    settingController.isFullScreen = IsFullScreen;
        //    Debug.Log(settingController.isFullScreen);
        //}
    }

    
    
}
