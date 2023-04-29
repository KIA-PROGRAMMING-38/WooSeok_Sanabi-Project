using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenChange : MonoBehaviour
{
    [SerializeField] private TMP_Text fullscreenText;

    private int fullScreenOn = 1;
    private int fullScreenOff = -1;

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


    private void InitFullscreen()
    {
        PlayerPrefs.SetInt("isFullScreen", fullScreenOn);
        Screen.fullScreen = true;
        fullscreenText.text = "ÄÑÁü";
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

    }

    
    
}
