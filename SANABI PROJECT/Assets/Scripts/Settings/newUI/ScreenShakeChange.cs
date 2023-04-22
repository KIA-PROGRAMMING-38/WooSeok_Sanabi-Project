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
        //InitShakeIntensity();
    }
    public void ConveyShakeIntensity()
    {
        shakeSlider.value = GameManager.Instance.ScreenShakeIntensity;
    }


    private void InitShakeIntensity()
    {
        shakeSlider.value = 1f;
        shakeSlider.value = GameManager.Instance.ScreenShakeIntensity;
    }


    private void Update()
    {
        //Debug.Log(GameManager.Instance.ScreenShakeIntensity);
    }
}
