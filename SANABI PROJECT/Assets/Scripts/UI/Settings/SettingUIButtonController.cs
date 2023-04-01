using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIButtonController : MonoBehaviour
{
    Color hovorColor = new Color(255, 0, 170);
    Color idleColor = Color.white;
    List<Image> images;
    void Start()
    {
        SettingUIPanelController.ChangeColor -= ChangeToHoverColour;
        SettingUIPanelController.ChangeColor += ChangeToHoverColour;

        images = new List<Image>();

        images.Add(gameObject.GetComponent<Image>());
        images.Add(transform.GetChild(0).gameObject.GetComponent<Image>());
    }

    void Update()
    {
        
    }

    private void ChangeToHoverColour(bool isHover)
    {
        if (isHover)
        {
            foreach (Image element in images)
            {
                element.color = hovorColor;
            }
        }
        else
        {
            foreach (Image element in images)
            {
                element.color = idleColor;
            }
        }
    }
}
