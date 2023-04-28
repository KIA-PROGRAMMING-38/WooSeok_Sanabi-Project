using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject QTESlider;
    public QTEUISlider sliderScript;
    public GameObject appearTextImage;
    public GameObject menaceTextImage;
    public GameObject beggingTextImage;
    public BoxCollider2D ceilingCollider;

    public bool isClickPhase { get; set; }
    public bool isAllPhaseFinished { get; set; }
    
    
    public Transform bossTransform { private get; set; }    

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.bossCanvasController = this;
        }
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

    public void TurnOffCeilingCollider()
    {
        ceilingCollider.enabled = false;
    }
    public void TurnOnAppearText()
    {
        appearTextImage.SetActive(true);
    }

    public void TurnOffAppearText()
    {
        appearTextImage.SetActive(false);
    }
    public void TurnOnMenaceText()
    {
        menaceTextImage.SetActive(true);
    }

    public void TurnOffMenaceText()
    {
        menaceTextImage.SetActive(false);
    }
    public void TurnOnBeggingText()
    {
        beggingTextImage.SetActive(true);
    }

    public void TurnOffBeggingText()
    {
        beggingTextImage.SetActive(false);
    }
}
