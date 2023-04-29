using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShakeChange : MonoBehaviour
{
    private Slider shakeSlider;
    private float initialScreenShakeValue = 1f;
    float ShakeIntensity;

    private void Awake()
    {
        shakeSlider = GetComponentInChildren<Slider>();
        
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("shakeIntensity"))
        {
            LoadShakeIntensity();
        }
        else
        {
            InitShakeIntensity();
        }
        
    }
    


    private void InitShakeIntensity()
    {
        PlayerPrefs.SetFloat("shakeIntensity", initialScreenShakeValue);
        shakeSlider.value = initialScreenShakeValue;
    }

    private void LoadShakeIntensity()
    {
        ShakeIntensity = PlayerPrefs.GetFloat("shakeIntensity");
        shakeSlider.value = ShakeIntensity;
    }
    public void ConveyShakeIntensity()
    {
        ShakeIntensity = shakeSlider.value;
        PlayerPrefs.SetFloat("shakeIntensity", ShakeIntensity);
    }


    private void Update()
    {
        
    }
}
