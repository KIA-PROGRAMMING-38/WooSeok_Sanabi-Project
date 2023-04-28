using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text[] textComponents;
    private Image[] imageComponents;
    private Color hoverColor;
    private Color idleColor;


    private void Awake()
    {
        textComponents = GetComponentsInChildren<TMP_Text>();
        imageComponents = GetComponentsInChildren<Image>();
        hoverColor = new Color(255, 0, 225);
        idleColor = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (TMP_Text element in textComponents)
        {
            element.color = hoverColor;
        }
        foreach (Image element in imageComponents)
        {
            if (element.gameObject == this.gameObject)
            {
                continue;
            }
            element.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (TMP_Text element in textComponents)
        {
            element.color = idleColor;
        }
        foreach (Image element in imageComponents)
        {
            if (element.gameObject == this.gameObject)
            {
                continue;
            }
            element.color = idleColor;
        }
    }
}
