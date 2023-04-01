using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUITextController : MonoBehaviour
{
    Text textComponent;
    Color hovorColor = new Color(255, 0, 170);
    Color idleColor = Color.white;
    void Start()
    {
        SettingUIPanelController.ChangeColor -= ChangeToHoverColour;
        SettingUIPanelController.ChangeColor += ChangeToHoverColour;
       
        textComponent = GetComponent<Text>();
    }

    void Update()
    {
        
    }

    private void ChangeToHoverColour(bool isHover)
    {
        if (isHover)
        {
            textComponent.color = hovorColor;
        }
        else
        {
            textComponent.color = idleColor;
        }
        
    }
    
}
