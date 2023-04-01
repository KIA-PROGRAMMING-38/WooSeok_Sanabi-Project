using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject[] textChildren;
    private void Awake()
    {
        textChildren = GameObject.FindGameObjectsWithTag("ButtonText");
        foreach (GameObject child in textChildren)
        {
            child.AddComponent<UIColorChange>();
        }
    }
    
}
