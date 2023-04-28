using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    [SerializeField] private FullScreenChange fullScreenChange;
    [SerializeField] private TMP_Text resolutionText;
    //private Button[] buttons;
    private int currentIndex;
    private bool isFullScreen;
    private int resolutionWidth;
    private int resolutionHeight;

    private int fullScreenOn = 1;
    private int fullScreenOff = -1;

    private (int, int, int)[] resolutionCandidates = new (int, int, int)[] {(960, 720, 144), (1024, 768, 144), (1280,720,144), (1440,900,144), (1600,900,144), (1920,1080,144) };

    private void Awake()
    {
        AddResolutions();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("resolutionWidth") && PlayerPrefs.HasKey("resolutionHeight"))
        {
            LoadResolution();
        }
        else
        {
            InitResolution();
        }
    }

    // playerPref 에서 받아와야할 건 width와 height 만 가져오면 될듯?

    private void AddResolutions()
    {
        for (int i = 0; i < resolutionCandidates.Length ;++i)
        {
            Resolution newResolution = new Resolution();
            newResolution.width = resolutionCandidates[i].Item1;
            newResolution.height = resolutionCandidates[i].Item2;
            newResolution.refreshRate = resolutionCandidates[i].Item3;
            resolutions.Add(newResolution);
        }
    }
    private void InitResolution()
    {
        resolutionWidth = 1920;
        resolutionHeight = 1080;
        resolutionText.text = $"{resolutionWidth}X{resolutionHeight}";
        Screen.SetResolution(resolutionWidth, resolutionHeight, true);
        PlayerPrefs.SetInt("resolutionWidth", resolutionWidth);
        PlayerPrefs.SetInt("resolutionHeight", resolutionHeight);
        PlayerPrefs.SetInt("resolutionIndex", resolutions.Count-1); // 5 at first
    }

    private void LoadResolution()
    {
        resolutionWidth = PlayerPrefs.GetInt("resolutionWidth");
        resolutionHeight = PlayerPrefs.GetInt("resolutionHeight");

        bool isFullScreen = default;
        if (PlayerPrefs.GetInt("isFullScreen") == fullScreenOn)
        {
            isFullScreen = true;
        }
        else
        {
            isFullScreen = false;
        }

        Screen.SetResolution(resolutionWidth, resolutionHeight, isFullScreen);
        resolutionText.text = $"{resolutionWidth}X{resolutionHeight}";
    }

    public void OnRightClick()
    {
        currentIndex = PlayerPrefs.GetInt("resolutionIndex");
        int changedIndex = (currentIndex + 1) % resolutions.Count;

        resolutionWidth = resolutionCandidates[changedIndex].Item1;
        resolutionHeight = resolutionCandidates[changedIndex].Item2;
        bool isFullScreen = default;
        if (PlayerPrefs.GetInt("isFullScreen") == fullScreenOn)
        {
            isFullScreen = true;
        }
        else
        {
            isFullScreen = false;
        }
        resolutionText.text = $"{resolutionWidth}X{resolutionHeight}";
        Screen.SetResolution(resolutionWidth, resolutionHeight, isFullScreen);
        PlayerPrefs.SetInt("resolutionWidth", resolutionWidth);
        PlayerPrefs.SetInt("resolutionHeight", resolutionHeight);
        PlayerPrefs.SetInt("resolutionIndex", changedIndex);

        //currentIndex = (currentIndex + 1) % resolutions.Count;
        //isFullScreen = fullScreenChange.IsFullScreen;
        //resolutionText.text = $"{resolutions[currentIndex].width}X{resolutions[currentIndex].height}";
        //Screen.SetResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, isFullScreen);
    }

    public void OnLeftClick()
    {
        currentIndex = PlayerPrefs.GetInt("resolutionIndex");
        int changedIndex = currentIndex - 1;

        if (changedIndex < 0)
        {
            changedIndex = resolutions.Count - 1;
        }

        resolutionWidth = resolutionCandidates[changedIndex].Item1;
        resolutionHeight = resolutionCandidates[changedIndex].Item2;
        bool isFullScreen = default;
        if (PlayerPrefs.GetInt("isFullScreen") == fullScreenOn)
        {
            isFullScreen = true;
        }
        else
        {
            isFullScreen = false;
        }
        resolutionText.text = $"{resolutionWidth}X{resolutionHeight}";
        Screen.SetResolution(resolutionWidth, resolutionHeight, isFullScreen);
        PlayerPrefs.SetInt("resolutionWidth", resolutionWidth);
        PlayerPrefs.SetInt("resolutionHeight", resolutionHeight);
        PlayerPrefs.SetInt("resolutionIndex", changedIndex);

        //currentIndex -= 1;

        //if (currentIndex < 0)
        //{
        //    currentIndex = resolutions.Count - 1;
        //}
        //isFullScreen = fullScreenChange.IsFullScreen;
        //resolutionText.text = $"{resolutions[currentIndex].width}X{resolutions[currentIndex].height}";
        //Screen.SetResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, isFullScreen);
    }
    
}
