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
            fullscreenText.text = "����";
        }
        else
        {
            fullscreenText.text = "����";
        }
    }

    //private void InitFullscreen()
    //{
    //    isFullScreen = true;
    //    IsFullScreen = isFullScreen;
    //    Screen.fullScreen = IsFullScreen;
    //    fullscreenText.text = "����";
    //}

    public void OnClick()
    {
        if (isFullScreen == true)
        {
            isFullScreen = false;
            IsFullScreen = isFullScreen;
            Screen.fullScreen = IsFullScreen;
            fullscreenText.text = "����";
            settingController.isFullScreen = IsFullScreen;
            Debug.Log(settingController.isFullScreen);
        }
        else
        {
            isFullScreen = true;
            IsFullScreen = isFullScreen;
            Screen.fullScreen = IsFullScreen;
            fullscreenText.text = "����";
            settingController.isFullScreen = IsFullScreen;
            Debug.Log(settingController.isFullScreen);
        }
    }

    
    
}
