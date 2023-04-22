using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour
{
    private List<Resolution> resolutions = new List<Resolution>();
    [SerializeField] private TMP_Text resoltionText;
    private Button[] buttons;
    private int currentIndex;
    private bool isFullScreen = true;

    private (int, int, int)[] resolutionCandidates = new (int, int, int)[] {(960, 720, 144), (1024, 768, 144), (1280,720,144), (1440,900,144), (1600,900,144), (1920,1080,144) };

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        AddResolutions();
        InitResolution();
    }

    private void InitResolution()
    {
        Screen.SetResolution(1920, 1080, true);
        currentIndex = resolutionCandidates.Length - 1;
    }
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

    public void OnRightClick()
    {
        currentIndex = (currentIndex + 1) % resolutions.Count;

        resoltionText.text = $"{resolutions[currentIndex].width}X{resolutions[currentIndex].height}";
        Screen.SetResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, isFullScreen);
    }

    public void OnLeftClick()
    {
        currentIndex -= 1;

        if (currentIndex < 0)
        {
            currentIndex = resolutions.Count - 1;
        }

        resoltionText.text = $"{resolutions[currentIndex].width}X{resolutions[currentIndex].height}";
        Screen.SetResolution(resolutions[currentIndex].width, resolutions[currentIndex].height, isFullScreen);
    }
    
}
