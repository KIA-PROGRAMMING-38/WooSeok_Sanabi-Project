using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject QTESlider;
    public QTEUISlider sliderScript;

    public bool isClickPhase { get; set; }
    public bool isAllPhaseFinished { get; set; }
    

    public Transform bossTransform { private get; set; }    

    private void OnEnable()
    {
        GameManager.Instance.bossCanvasController = this;
        isClickPhase = true;
    }

    private void Start()
    {
        sliderScript.OnFinishClickPhase -= TurnOffSlider;
        sliderScript.OnFinishClickPhase += TurnOffSlider;
        sliderScript.OnFinishAllPhase -= TurnOffSlider;
        sliderScript.OnFinishAllPhase += TurnOffSlider;
        sliderScript.OnFailAnyPhase -= TurnOffSlider;
        sliderScript.OnFailAnyPhase += TurnOffSlider;
        GameManager.Instance.playerController.OnQTEHitFinished -= TurnOnSlider;
        GameManager.Instance.playerController.OnQTEHitFinished += TurnOnSlider;
    }

    public void IncreaseSliderGuageWhenClick()
    {
        if (isClickPhase)
        {
            sliderScript.IncreaseClickSliderGuage();
        }
    }

    public void IncreaseSliderGuageWhenHold()
    {
        if (!isClickPhase)
        {
            sliderScript.IncreaseHoldSliderGuage();
        }
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
