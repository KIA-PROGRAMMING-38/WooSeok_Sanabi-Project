using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUISliderController : MonoBehaviour
{
    Color hovorColor = new Color(255, 0, 170);
    Color idleColor = Color.white;
    List<Image> images;
    
    List<GameObject> imageContainingObjects;
    void Start()
    {
        SettingUIPanelController.ChangeColor -= ChangeToHoverColour;
        SettingUIPanelController.ChangeColor += ChangeToHoverColour;
        

        images = new List<Image>();
        for (int i = 0; i < transform.childCount ;++i)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("ImageContain"))
            {
                images.Add(child.GetComponent<Image>());
            }
            else
            {
                images.Add(child.transform.GetChild(0).gameObject.GetComponent<Image>());
            }
        }

        

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
