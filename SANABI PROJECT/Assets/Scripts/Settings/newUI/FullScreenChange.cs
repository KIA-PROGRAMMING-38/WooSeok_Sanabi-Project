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
        fullscreenText.text = "����";
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

    }

    
    
}
