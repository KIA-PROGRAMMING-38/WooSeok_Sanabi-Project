using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public readonly string HORIZONTAL = "Horizontal";
    public readonly string JUMP = "Jump";
    public float horizontalInput;
    void Start()
    {
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw(HORIZONTAL);
    }
}
