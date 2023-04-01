using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNumber
{
    Normal,
    Settings,
    Exit,
}

public class TitleUIManager : MonoBehaviour
{
    GameObject[] buttonChildren;
    TitleUIButtonController buttonController;
    private void Awake()
    {
        buttonChildren = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < buttonChildren.Length; ++i)
        {
            buttonChildren[i].AddComponent<TitleUIButtonController>();
            buttonController = buttonChildren[i].GetComponent<TitleUIButtonController>();
            buttonController.sceneID = i;
        }
    }

    
}
