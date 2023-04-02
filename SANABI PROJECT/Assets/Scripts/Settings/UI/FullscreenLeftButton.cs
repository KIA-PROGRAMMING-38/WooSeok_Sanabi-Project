using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FullscreenLeftButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Canvas UICanvas;
    SettingSceneController sceneController;
    public void OnPointerClick(PointerEventData eventData)
    {
        sceneController.isFullScreen = false;
    }

    void Start()
    {
        sceneController = GetComponent<SettingSceneController>();
    }

    void Update()
    {
        
    }
}
