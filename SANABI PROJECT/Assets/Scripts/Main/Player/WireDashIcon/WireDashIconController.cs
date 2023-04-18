using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDashIconController : MonoBehaviour
{
    [SerializeField] private Transform wireDashIconPosition;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = wireDashIconPosition.position;
    }
}
