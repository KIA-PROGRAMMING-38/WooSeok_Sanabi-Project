using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingTextController : MonoBehaviour, IColorChange
{
    private Text textComponent;
    void Start()
    {
        SettingPanelController.OnHover -= ColorChange;
        SettingPanelController.OnHover += ColorChange;
        textComponent = gameObject.GetComponent<Text>();
    }

    public void ColorChange(bool isHover)
    {
        if (isHover)
        {
            textComponent.color = GameManager.hoverColor;
        }
        else
        {
            textComponent.color = GameManager.idleColor;
        }
    }
}
