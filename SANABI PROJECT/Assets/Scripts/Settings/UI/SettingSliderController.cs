using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSliderController : MonoBehaviour
{
    Slider slider;
    private void OnEnable()
    {
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        //GameManager.shakeIntensity = slider.value;
    }
}
