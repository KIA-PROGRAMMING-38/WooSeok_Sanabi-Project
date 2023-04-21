using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    private Color hoverColor;
    private Color idleColor;
    private void Awake()
    {
        text = GetComponentInChildren<Text>();
        hoverColor = new Color(255, 0, 225);
        idleColor = Color.white;
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = idleColor;
    }
}
