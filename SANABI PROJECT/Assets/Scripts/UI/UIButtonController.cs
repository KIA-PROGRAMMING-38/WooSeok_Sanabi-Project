using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour, IPointerClickHandler
{
    Camera mainCamera;
    public int sceneID { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Object.DontDestroyOnLoad(mainCamera); // 화면 흔들림은 계속 필요해서 매씬 데려갈꺼임

        if (sceneID == (int)SceneNumber.Settings)
        {
            SceneManager.LoadScene(sceneID, LoadSceneMode.Additive);
            return;
        }
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
        mainCamera= Camera.main;
    }

    

}
