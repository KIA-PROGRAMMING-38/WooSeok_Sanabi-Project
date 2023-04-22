using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textBackground : MonoBehaviour
{
    private TMP_Text textComponent;
    [SerializeField] [Range(0f, 0.5f)] private float reappearTime = 0.001f;
    private WaitForSeconds _reappearTime;
    private IEnumerator _ShowTextBackground;
    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        _reappearTime = new WaitForSeconds(reappearTime);
        
    }
    
    private void OnEnable()
    {
        textComponent.enabled = true;
        StartShowingTextBackground();
    }


    private void StartShowingTextBackground()
    {
        if (_ShowTextBackground == null)
        {
            _ShowTextBackground = ShowTextBackground();
        }
        StartCoroutine(_ShowTextBackground);
    }

    private void StopShowingTextBackground()
    {
        StopCoroutine(_ShowTextBackground);
    }

    private IEnumerator ShowTextBackground()
    {
        while (true)
        {
            textComponent.enabled = false;
            yield return _reappearTime;
            textComponent.enabled = true;
            yield return _reappearTime;
        }
    }

    private void OnDisable()
    {
        //StopShowingTextBackground();
    }
}
