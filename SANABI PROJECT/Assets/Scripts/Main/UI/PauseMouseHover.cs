using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private Text text;
    private TMP_Text textComponent;
    private Color hoverColor;
    private Color idleColor;
    private void Awake()
    {
        //text = GetComponentInChildren<Text>();
        textComponent = GetComponentInChildren<TMP_Text>();
        hoverColor = new Color(255, 0, 225);
        idleColor = Color.white;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        //text.color = hoverColor;
        textComponent.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //text.color = idleColor;
        textComponent.color = idleColor;
    }
}
