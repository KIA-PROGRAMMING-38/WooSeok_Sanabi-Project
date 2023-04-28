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
        //    fullscreenText.text = "����";
        //}
        //else
        //{
        //    fullscreenText.text = "����";
        //}
    }

    private void InitFullscreen()
    {
        PlayerPrefs.SetInt("isFullScreen", fullScreenOn);
        Screen.fullScreen = true;
        fullscreenText.text = "����";
        //isFullScreen = true;
        //IsFullScreen = isFullScreen;
        //Screen.fullScreen = IsFullScreen;
        //fullscreenText.text = "����";
    }

    private void LoadFullScreenState()
    {
        int fullScreenValue = PlayerPrefs.GetInt("isFullScreen");
        if (fullScreenValue == fullScreenOn)
        {
            Screen.fullScreen = true;
            fullscreenText.text = "����";
        }
        else
        {
            Screen.fullScreen = false;
            fullscreenText.text = "����";
        }
    }


    public void OnClick()
    {
        if (PlayerPrefs.GetInt("isFullScreen") == fullScreenOn)
        {
            PlayerPrefs.SetInt("isFullScreen", fullScreenOff);
            Screen.fullScreen = false;
            fullscreenText.text = "����";
        }
        else // if not full screen but clicked the button?
        {
            PlayerPrefs.SetInt("isFullScreen", fullScreenOn);
            Screen.fullScreen = true;
            fullscreenText.text = "����";
        }


        //if (isFullScreen == true)
        //{
        //    isFullScreen = false;
        //    IsFullScreen = isFullScreen;
        //    Screen.fullScreen = IsFullScreen;
        //    fullscreenText.text = "����";
        //    settingController.isFullScreen = IsFullScreen;
        //    Debug.Log(settingController.isFullScreen);
        //}
        //else
        //{
        //    isFullScreen = true;
        //    IsFullScreen = isFullScreen;
        //    Screen.fullScreen = IsFullScreen;
        //    fullscreenText.text = "����";
        //    settingController.isFullScreen = IsFullScreen;
        //    Debug.Log(settingController.isFullScreen);
        //}
    }

    
    
}
