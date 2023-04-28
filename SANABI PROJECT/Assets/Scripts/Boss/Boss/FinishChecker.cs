using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishChecker : MonoBehaviour
{
    private bool isPlayerInFinishRange;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.bossController.isBossReadyToBeFinished)
        {
            isPlayerInFinishRange = true;
            if (!GameManager.Instance.bossController.isBossDead)
            {
                GameManager.Instance.bossCanvasController.TurnOnBeggingText();
            }
            GameManager.Instance.cameraFollow.StartEternalZoomInPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.bossController.isBossReadyToBeFinished)
        {
            isPlayerInFinishRange = false;
            GameManager.Instance.bossCanvasController.TurnOffBeggingText();
            GameManager.Instance.cameraFollow.StopEternalZoomOutPlayer();
        }
    }

    public bool IsPlayerInFinishRange()
    {
        return isPlayerInFinishRange;
    }
}
