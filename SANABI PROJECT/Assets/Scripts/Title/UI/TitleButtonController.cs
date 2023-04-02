using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleButtonController : TitleSceneController, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Text textComponent;
    [SerializeField] private int buttonID;
    public int ButtonID
    {
        private get => buttonID;
        set => buttonID = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (buttonID == (int)SceneNumber.Settings)
        {
            SceneManager.LoadScene(buttonID, LoadSceneMode.Additive);
            return;
        }
        if (buttonID == (int)SceneNumber.Exit)
        {
            Application.Quit();
            return;
        }
        
        SceneManager.LoadScene(buttonID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textComponent.color = WholeSceneManager.hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textComponent.color = WholeSceneManager.idleColor;
    }

    private void OnEnable()
    {
        textComponent = GetComponentInChildren<Text>();
    }
}
