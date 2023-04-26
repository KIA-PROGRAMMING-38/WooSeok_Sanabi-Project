using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEUISlider : MonoBehaviour
{

    private Vector2 offset = new Vector2(0f, 3f);

    [SerializeField] private float offSetTime = 0.5f;
    [SerializeField] private float initialValue = 0.1f;
    [SerializeField] private float increaseAmount = 0.08f;
    [SerializeField] private float increaseHoldMultiplier = 0.05f;
    [SerializeField] private float decreaseAmount = 0.05f;
    [SerializeField] private float decreaseTimeGap = 0.1f;

    private WaitForSeconds _offSetTime;
    private WaitForSeconds _decreaseTimeGap;
    private IEnumerator _DecreaseSliderGuage;

    public bool isClickPhase { get; private set; }

    public event Action OnFinishClickPhase;
    public event Action OnFinishAllPhase; // finish 햇을대 event 넘겨줄 것
    public event Action OnFailAnyPhase; // fail 했을대 evetn 넘겨줄것


    public Transform bossTransform { private get; set; }
    private Animator iconAnimator;
    private Slider QTESlider;

    private void Awake()
    {
        QTESlider = GetComponent<Slider>();
        iconAnimator = GetComponentInChildren<Animator>();
        _offSetTime = new WaitForSeconds(offSetTime);
        _decreaseTimeGap = new WaitForSeconds(decreaseTimeGap);
        _DecreaseSliderGuage = DecreaseSliderGuage();
    }

    private void OnEnable()
    {
        isClickPhase = GameManager.Instance.bossCanvasController.isClickPhase;

        if (isClickPhase)
        {
            iconAnimator.SetBool("click", true);
        }
        else
        {
            iconAnimator.SetBool("hold", true);
        }
        

        if (bossTransform != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(bossTransform.position + (Vector3)offset);
        }

        QTESlider.value = initialValue; // default value

        if (_DecreaseSliderGuage == null)
        {
            _DecreaseSliderGuage = DecreaseSliderGuage();
        }

        StartCoroutine(_DecreaseSliderGuage);
    }

    private void Update()
    {
        if (isClickPhase) // if click phase
        {
            if (1f <= QTESlider.value)
            {
                isClickPhase = false;
                GameManager.Instance.bossCanvasController.isClickPhase = false;
                iconAnimator.SetBool("click", false);
                OnFinishClickPhase?.Invoke();
                //iconAnimator.SetBool("hold", true);
                QTESlider.value = initialValue;
            }
            else if (QTESlider.value <= 0f) // if fail.....
            {
                Debug.Log($"클릭 phase에서 0 도달");
                OnFailAnyPhase?.Invoke();
            }
        }
        else // if hold phase
        {
            if (1f <= QTESlider.value)
            {
                isClickPhase = true;
                GameManager.Instance.bossCanvasController.isClickPhase = true;
                iconAnimator.SetBool("hold", false);
                OnFinishAllPhase?.Invoke();
            }
            else if (QTESlider.value <= 0f) // if fail.....
            {
                Debug.Log($"홀드 phase에서 0 도달");
                OnFailAnyPhase?.Invoke();
            }
        }

    }

    public void IncreaseClickSliderGuage()
    {
        QTESlider.value += increaseAmount;
    }

    public void IncreaseHoldSliderGuage()
    {
        QTESlider.value += increaseAmount * increaseHoldMultiplier;
    }

    private IEnumerator DecreaseSliderGuage()
    {
        yield return _offSetTime;

        while (true)
        {
            yield return _decreaseTimeGap;
            QTESlider.value -= decreaseAmount;
        }
    }


}
