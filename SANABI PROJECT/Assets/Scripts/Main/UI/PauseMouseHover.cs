using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private Text text;
    private TMP_Text[] textComponents;
    private Color hoverColor;
    private Color idleColor;
    private void Awake()
    {
        textComponents = GetComponentsInChildren<TMP_Text>();
        hoverColor = new Color(255, 0, 225);
        idleColor = Color.white;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (TMP_Text element in textComponents)
        {
            element.color = hoverColor;
        }
        //textComponents.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (TMP_Text element in textComponents)
        {
            element.color = idleColor;
        }
        //textComponents.color = idleColor;
    }
}
