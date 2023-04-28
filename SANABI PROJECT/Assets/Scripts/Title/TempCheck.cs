using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCheck : MonoBehaviour
{
    public GameObject titleSceneController;
    
    public void TurnOnTitleCanvas()
    {
        titleSceneController.SetActive(true);
    }
}
