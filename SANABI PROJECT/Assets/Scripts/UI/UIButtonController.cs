using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour, IPointerClickHandler
{
    public int sceneID { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sceneID == (int)SceneNumber.Exit)
        {
            Application.Quit(); // 게임을 종료하는 코드
            return;
        }

        SceneManager.LoadScene(sceneID);
    }

    private void Start()
    {
        transform.GetChild(0).AddComponent<UITextController>();
    }

    

}
