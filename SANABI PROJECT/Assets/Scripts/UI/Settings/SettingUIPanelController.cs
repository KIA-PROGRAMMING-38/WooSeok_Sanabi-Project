using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SettingUIPanelController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform[] children;
    private int childrenCount;
    GameObject textChild;
    Text textChildComponent;

    GameObject sliderChild;
    Slider sliderChildComponent;


    GameObject buttonChild;
    Button buttonChildComponent;

    public static event Action<bool> ChangeColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeColor.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeColor.Invoke(false);
    }

    void Start()
    {
        childrenCount = transform.childCount;
        children = new Transform[childrenCount];

        for (int i = 0; i < childrenCount; ++i)
        {
            children[i] = transform.GetChild(i);
            if (children[i].gameObject.CompareTag(SettingUIManager.TEXT))
            {
                textChild = children[i].gameObject;
                textChildComponent = textChild.GetComponent<Text>();
            }
            else if (children[i].gameObject.CompareTag(SettingUIManager.SLIDER))
            {
                sliderChild = children[i].gameObject;
                sliderChildComponent = sliderChild.GetComponent<Slider>();
            }
            else if (children[i].gameObject.CompareTag(SettingUIManager.BUTTON))
            {
                buttonChild = children[i].gameObject;
                buttonChildComponent = buttonChild.GetComponent<Button>();
            }
        }
    }
    
    void Update()
    {

    }
}
