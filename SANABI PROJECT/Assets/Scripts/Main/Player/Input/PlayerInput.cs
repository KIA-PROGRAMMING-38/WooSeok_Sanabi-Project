using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public readonly string HORIZONTAL = "Horizontal";
    public readonly string VERTICAL = "Vertical";
    public readonly string JUMP = "Jump";
    private float horizontalInput;
    private float verticalInput;
    
    public Vector2 MovementInput { get; private set; }
    void Start()
    {
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw(HORIZONTAL);
        verticalInput = Input.GetAxisRaw(VERTICAL);
        MovementInput = new Vector2(horizontalInput, verticalInput);
    }
}
