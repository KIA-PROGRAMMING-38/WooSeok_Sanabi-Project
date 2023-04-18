using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingImageController : MonoBehaviour, IColorChange
{
    Image imageComponent;
    
    void Start()
    {
        SettingPanelController.OnHover -= ColorChange;
        SettingPanelController.OnHover += ColorChange;
        imageComponent = gameObject.GetComponent<Image>();
    }

    public void ColorChange(bool isHover)
    {
        //if (isHover)
        //{
        //    imageComponent.color = GameManager.hoverColor;
        //}
        //else
        //{
        //    imageComponent.color = GameManager.idleColor;
        //}
    }
}
