using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeText;
    //private Button[] buttons;

    private float curVolume;
    private float minVolume = 0f;
    private float maxVolume = 10f;

    private void Awake()
    {
        InitVolume();
    }

    private void Start()
    {
        
    }

    private void InitVolume()
    {
        curVolume = 5f;
        volumeText.text = $"{curVolume}";
        AudioListener.volume = curVolume / maxVolume;
    }

    public void OnRightClick()
    {
        curVolume += 1f;
        if (maxVolume < curVolume)
        {
            curVolume = minVolume;
        }
        volumeText.text = $"{curVolume}";
        AudioListener.volume = curVolume / maxVolume;
    }

    public void OnLeftClick()
    {
        curVolume -= 1f;
        if (curVolume < minVolume)
        {
            curVolume = maxVolume;
        }
        volumeText.text = $"{curVolume}";
        AudioListener.volume = curVolume / maxVolume;
    }


}
