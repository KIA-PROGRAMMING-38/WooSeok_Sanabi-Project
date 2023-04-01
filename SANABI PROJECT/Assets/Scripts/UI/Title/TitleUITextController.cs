using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleUITextController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text textComponent;
    Color hovorColor = new Color(255, 0, 170);
    Color idleColor = Color.white;

    private void Start()
    {
        textComponent= GetComponent<Text>();
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

