using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNumber
{
    Normal,
    Settings,
    Exit,
}

public class UIManager : MonoBehaviour
{
    GameObject[] buttonChildren;
    UIButtonController buttonController;
    private void Awake()
    {
        buttonChildren = GameObject.FindGameObjectsWithTag("Button");
        for (int i = 0; i < buttonChildren.Length; ++i)
        {
            buttonChildren[i].AddComponent<UIButtonController>();
            buttonController = buttonChildren[i].GetComponent<UIButtonController>();
            buttonController.sceneID = i;
        }
    }

    
}
