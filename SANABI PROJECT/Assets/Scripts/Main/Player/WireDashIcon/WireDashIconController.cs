using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDashIconController : MonoBehaviour
{
    [SerializeField] private Transform wireDashIconPosition;
    private Animator wireDashIconAnimator;

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
        wireDashIconAnimator.SetTrigger("on");
    }


    private void StartIconWhile()
    {
        wireDashIconAnimator.SetBool("while", true);
    }

    public void StartIconOff()
    {

        wireDashIconAnimator.SetBool("while", false);
        wireDashIconAnimator.SetTrigger("off");

    }

}
