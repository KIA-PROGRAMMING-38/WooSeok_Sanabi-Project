using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUIManager : MonoBehaviour
{
    public readonly static string BUTTON = "Button";// 오타 방지용
    public readonly static string SLIDER = "Slider";
    public readonly static string TEXT = "Text";
    public readonly static string PANEL = "Panel"; 
    GameObject[] buttonChildren;
    GameObject[] sliderChildren;
    GameObject[] textChildren;
    GameObject[] panelChildren;
    private void Awake()
    {
        // 어차피 자식이 얼마 없어서 Find 썼음(문제가 되려나?)
        panelChildren = GameObject.FindGameObjectsWithTag(PANEL);
        buttonChildren = GameObject.FindGameObjectsWithTag(BUTTON);
        sliderChildren = GameObject.FindGameObjectsWithTag(SLIDER);
        textChildren = GameObject.FindGameObjectsWithTag(TEXT);

        for (int i = 0; i < panelChildren.Length ;++i)
        {
            panelChildren[i].AddComponent<SettingUIPanelController>();
        }
        
        for (int i = 0; i < buttonChildren.Length; ++i)
        {
            buttonChildren[i].AddComponent<SettingUIButtonController>();
        }

        for (int i = 0; i < sliderChildren.Length; ++i)
        {
            sliderChildren[i].AddComponent<SettingUISliderController>();
        }

        for (int i = 0; i < textChildren.Length; ++i)
        {
            textChildren[i].AddComponent<SettingUITextController>();
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
