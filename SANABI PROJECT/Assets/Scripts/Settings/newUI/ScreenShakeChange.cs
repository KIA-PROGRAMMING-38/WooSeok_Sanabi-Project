using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShakeChange : MonoBehaviour
{
    private Slider shakeSlider;

    private void Awake()
    {
        shakeSlider = GetComponentInChildren<Slider>();
        
    }

    private void Start()
    {
        InitShakeIntensity();
    }
    public void ConveyShakeIntensity()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ScreenShakeIntensity = shakeSlider.value;
        }
        //GameManager.Instance.ScreenShakeIntensity = shakeSlider.value;
    }


    private void InitShakeIntensity()
    {
        shakeSlider.value = 1f;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ScreenShakeIntensity = shakeSlider.value;
        }
    }


    private void Update()
    {
        
    }
}
