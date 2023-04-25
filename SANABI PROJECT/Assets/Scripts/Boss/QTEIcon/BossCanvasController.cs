using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject QTESlider;
    [SerializeField] private QTEUISlider sliderScript;
    public Transform bossTransform { private get; set; }    

    private void OnEnable()
    {
        GameManager.Instance.bossCanvasController = this;
    }

    private void Start()
    {
        
    }

    public void TurnOnSlider()
    {
        sliderScript.bossTransform = bossTransform;
        QTESlider.SetActive(true);
    }

    public void TurnOffSlider()
    {
        QTESlider.SetActive(false);
    }

    private void Update()
    {
       
    }

}
