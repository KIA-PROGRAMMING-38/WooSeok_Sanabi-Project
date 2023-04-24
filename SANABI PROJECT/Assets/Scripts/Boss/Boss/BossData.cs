using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossData : MonoBehaviour
{
    [Header("Appear State")]
    [Range(10f, 20f)] public float appearSpeed = 10f;




    [Header("Check Variables")]
    public float groundCheckRadius = 0.5f;
    public LayerMask whatIsGround;
}
