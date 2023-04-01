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
        Object.DontDestroyOnLoad(mainCamera); // ȭ�� ��鸲�� ��� �ʿ��ؼ� �ž� ����������

        if (sceneID == (int)SceneNumber.Settings)
        {
            SceneManager.LoadScene(sceneID, LoadSceneMode.Additive);
            return;
        }
        if (sceneID == (int)SceneNumber.Exit)
        {
            Application.Quit(); // ������ �����ϴ� �ڵ�
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
