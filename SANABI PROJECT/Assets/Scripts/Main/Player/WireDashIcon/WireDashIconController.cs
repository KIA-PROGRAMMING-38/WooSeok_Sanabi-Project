using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDashIconController : MonoBehaviour
{
    [SerializeField] private Transform wireDashIconPosition;
    private Animator wireDashIconAnimator;
    private bool hasPlayerDashed;
    void Start()
    {
        wireDashIconAnimator = GetComponent<Animator>();
        GameManager.Instance.playerController.OnWireDash -= StartIconOn;
        GameManager.Instance.playerController.OnWireDash += StartIconOn;
        GameManager.Instance.playerController.OnWireDashFinished -= StartIconOff;
        GameManager.Instance.playerController.OnWireDashFinished += StartIconOff;
    }

    void Update()
    {
        transform.position = wireDashIconPosition.position;
        
    }

    private void StartIconOn()
    {
        wireDashIconAnimator.SetBool("on", true);
        hasPlayerDashed = true;
    }


    private void StartIconWhile()
    {
        wireDashIconAnimator.SetBool("on", false);
        if (hasPlayerDashed)
        {
            wireDashIconAnimator.SetBool("while", true);
        }
        
    }

    public void StartIconOff()
    {
        if (hasPlayerDashed)
        {
            wireDashIconAnimator.SetBool("while", false);
            wireDashIconAnimator.SetBool("off", true);
            hasPlayerDashed = false;
        }
    }

    private void StopIconOff()
    {
        wireDashIconAnimator.SetBool("off", false);
    }

}
