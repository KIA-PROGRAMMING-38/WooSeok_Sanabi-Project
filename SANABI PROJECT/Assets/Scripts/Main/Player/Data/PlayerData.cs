using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData : MonoBehaviour
{
    [Header("Run State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    private void Start()
    {
        whatIsGround = LayerMask.GetMask("NormalWall");
    }
}
