using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIButtonController : MonoBehaviour, IPointerClickHandler
{
    Camera mainCamera;
    public int sceneID { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
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
        transform.GetChild(0).AddComponent<TitleUITextController>(); // ������ �ڽ� �Ѹ�ۿ� ����
        mainCamera= Camera.main;
    }

    

}
