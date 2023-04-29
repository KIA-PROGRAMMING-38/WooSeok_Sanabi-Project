using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeText;
    //private Button[] buttons;

    private float initialVolume = 5f;
    private float minVolume = 0f;
    private float maxVolume = 10f;

    private void Awake()
    {
        //InitVolume();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            InitVolume();
        }
    }

    private void InitVolume()
    {
        volumeText.text = $"{initialVolume}";
        float actualVolume = initialVolume / maxVolume;
        AudioListener.volume = actualVolume;
        PlayerPrefs.SetFloat("musicVolume", actualVolume);
    }

    private void LoadVolume()
    {
        float actualVolume = PlayerPrefs.GetFloat("musicVolume");
        volumeText.text = $"{actualVolume * maxVolume}";
        AudioListener.volume = actualVolume;
        PlayerPrefs.SetFloat("musicVolume", actualVolume);
    }

    public void OnRightClick()
    {
        float increasedVolume = PlayerPrefs.GetFloat("musicVolume") * maxVolume + 1f;

        if (maxVolume < increasedVolume)
        {
            increasedVolume = minVolume;
        }
        volumeText.text = $"{increasedVolume}";
        float actualVolume = increasedVolume * 0.1f;
        AudioListener.volume = actualVolume;
        PlayerPrefs.SetFloat("musicVolume", actualVolume);
    }

    public void OnLeftClick()
    {
        float decreasedVolume = PlayerPrefs.GetFloat("musicVolume") * maxVolume - 1f;

        if (Mathf.Abs(decreasedVolume) < 0.00001f) // this is due to float precision problem
        {
            decreasedVolume = minVolume;
        }

        if (decreasedVolume < minVolume)
        {
            decreasedVolume = maxVolume;
        }
        volumeText.text = $"{decreasedVolume}";
        float actualVolume = decreasedVolume * 0.1f;

        AudioListener.volume = actualVolume;
        PlayerPrefs.SetFloat("musicVolume", actualVolume);
    }

    private void Update()
    {
        
    }

}
