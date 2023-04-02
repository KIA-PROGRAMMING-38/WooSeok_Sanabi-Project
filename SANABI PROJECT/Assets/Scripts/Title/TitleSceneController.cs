using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : WholeSceneManager
{
    GameObject[] buttons;
    TitleButtonController buttonController;
    private void OnEnable()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        for (int i= 0; i < buttons.Length ;++i)
        {
            buttons[i].AddComponent<TitleButtonController>();
            buttonController = buttons[i].GetComponent<TitleButtonController>();
            buttonController.ButtonID = i;
        }
    }



}
