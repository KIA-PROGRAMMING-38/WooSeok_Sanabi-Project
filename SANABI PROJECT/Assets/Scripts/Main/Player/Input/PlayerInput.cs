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

    public bool JumpInputStop { get; private set; }
    public bool JumpInput { get; private set; }
    public Vector2 MovementInput { get; private set; }

    
    void Start()
    {
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw(HORIZONTAL);
        verticalInput = Input.GetAxisRaw(VERTICAL);
        MovementInput = new Vector2(horizontalInput, verticalInput);

        
        JumpInput = Input.GetButtonDown(JUMP);
        JumpInputStop = Input.GetButtonUp(JUMP);
        
    }

    public void UseJumpInput() => JumpInput = false; 
    
    
}
