using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    Vector3 offSet;
    void Start()
    {
        offSet = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offSet;
    }
}
