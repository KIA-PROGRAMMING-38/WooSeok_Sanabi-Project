using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    Text textComponent;
    Color hovorColor = new Color(255, 0, 170);
    Color idleColor = Color.white;

    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = hovorColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = idleColor;
    }

}
