using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFollowCam : MonoBehaviour
{

    public Transform cameraRainPos;


    private void LateUpdate()
    {
        transform.position = cameraRainPos.position;
    }
}
