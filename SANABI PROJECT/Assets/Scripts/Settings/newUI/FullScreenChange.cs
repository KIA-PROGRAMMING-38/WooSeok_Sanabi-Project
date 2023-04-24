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
    private bool isFullScreen = true;
    public bool IsFullScreen { get; private set; }

    private void Awake()
    {
        //InitFullscreen();
        settingController = GetComponentInParent<SettingSceneController>();
    }

    private void OnEnable()
    {
        isFullScreen = settingController.isFullScreen;
        if (isFullScreen)
        {
            fullscreenText.text = "ÄÑÁü";
        }
        else
        {
            fullscreenText.text = "²¨Áü";
        }
    }

    //private void InitFullscreen()
    //{
    //    isFullScreen = true;
    //    IsFullScreen = isFullScreen;
    //    Screen.fullScreen = IsFullScreen;
    //    fullscreenText.text = "ÄÑÁü";
    //}

    public void OnClick()
    {
        if (isFullScreen == true)
        {
            isFullScreen = false;
            IsFullScreen = isFullScreen;
            Screen.fullScreen = IsFullScreen;
            fullscreenText.text = "²¨Áü";
            settingController.isFullScreen = IsFullScreen;
            Debug.Log(settingController.isFullScreen);
        }
        else
        {
            isFullScreen = true;
            IsFullScreen = isFullScreen;
            Screen.fullScreen = IsFullScreen;
            fullscreenText.text = "ÄÑÁü";
            settingController.isFullScreen = IsFullScreen;
            Debug.Log(settingController.isFullScreen);
        }
    }

    
    
}
