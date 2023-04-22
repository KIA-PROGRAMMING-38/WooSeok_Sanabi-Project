using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenChange : MonoBehaviour
{
    [SerializeField] private TMP_Text fullscreenText;
    //private Button[] buttons;
    private bool isFullScreen = true;
    public bool IsFullScreen { get; private set; }

    private void Awake()
    {
        InitFullscreen();
    }

    private void InitFullscreen()
    {
        isFullScreen = true;
        IsFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        fullscreenText.text = "ÄÑÁü";
    }

    public void OnClick()
    {
        if (isFullScreen == true)
        {
            isFullScreen = false;
            IsFullScreen = isFullScreen;
            Screen.fullScreen = isFullScreen;
            fullscreenText.text = "²¨Áü";
        }
        else
        {
            isFullScreen = true;
            IsFullScreen = isFullScreen;
            Screen.fullScreen = isFullScreen;
            fullscreenText.text = "ÄÑÁü";
        }
    }

    
    
}
