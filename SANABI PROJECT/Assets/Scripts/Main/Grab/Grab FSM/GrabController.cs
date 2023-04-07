using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public GrabStateMachine grabStateMachine { get; private set; }
    public GrabIdleState IdleState { get; private set; }
    public GrabFlyingState FlyingState { get; private set; }
    public GrabGrabbedState GrabbedState { get; private set; }
    public GrabReturningState ReturningState { get; private set; }


    public PlayerData playerData { get; private set; }
    public Rigidbody2D grabRigid { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public PlayerController playerController { get; private set; }  
    public CapsuleCollider2D capsuleCollider { get; private set; }
    
    public BoxCollider2D GrabReturnCollider;
    public TrailRenderer trailRenderer { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput playerInput { get; private set; }

    public bool HitNormal { get; set; }
    public bool HitNoGrab { get; set; }
    public float GrabMaxLength { get; private set; }
    public bool isGrappled { get; set; }

    public int NormalWallLayerNumber { get; private set; }
    public int NoGrabWallLayerNumber { get; private set; }
    public int MagmaLayerNumber { get; private set; }
    
    public Vector3 startPos { get; set; }
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 workSpace;
    public bool isGrabReturned { get; set; }
    public Vector2 flyDirection { get; private set; }
    public float flySpeed { get; private set; }
    public Quaternion flyRotation { get; private set; }
    private Vector2 chaseVector;
    private void Awake()
    {
        grabStateMachine = new GrabStateMachine();
        playerData = GetComponentInParent<PlayerData>();

        IdleState = new GrabIdleState(this, grabStateMachine, playerData, "idle");
        FlyingState = new GrabFlyingState(this, grabStateMachine, playerData, "flying");
        GrabbedState = new GrabGrabbedState(this, grabStateMachine, playerData, "grabbed");
        ReturningState = new GrabReturningState(this, grabStateMachine, playerData, "returning");
    }
    void Start()
    {
        grabRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        Animator = GetComponent<Animator>();
        playerInput = GetComponentInParent<PlayerInput>();  
        

        NormalWallLayerNumber = LayerMask.NameToLayer("NormalWall");
        NoGrabWallLayerNumber = LayerMask.NameToLayer("NoGrabWall");
        MagmaLayerNumber = LayerMask.NameToLayer("Magma");

        grabStateMachine.Initialize(IdleState);
        GrabMaxLength = playerData.wireLength;
    }

    void Update()
    {
        CurrentVelocity = grabRigid.velocity;
        grabStateMachine.grabCurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        grabStateMachine.grabCurrentState.PhysicsUpdate();
    }

    
    

    public void ResetGrab()
    {
        GrabReturnCollider.enabled = false;
        transform.position = startPos;
        DeactivateGrab();
    }
    public void SetGrabVelocity(float throwSpeed, Vector2 targetDirection, Quaternion shootRotation)
    {
        flySpeed = throwSpeed;
        flyDirection = targetDirection;
        flyRotation = shootRotation;
    }
    public void FlyGrab()
    {
        ActivateGrab();
        transform.rotation = flyRotation;
        workSpace.Set(flyDirection.x * flySpeed, flyDirection.y * flySpeed);
        grabRigid.velocity = workSpace;
        CurrentVelocity = workSpace;
    }

    public void ActivateGrab()
    {
        capsuleCollider.enabled = true;
        spriteRenderer.enabled = true;
        grabRigid.bodyType = RigidbodyType2D.Dynamic;
        grabRigid.gravityScale = 0f;
        trailRenderer.enabled = true;
    }

    public void DeactivateGrab()
    {
        workSpace = Vector2.zero;
        grabRigid.bodyType = RigidbodyType2D.Kinematic;
        grabRigid.velocity = workSpace;
        CurrentVelocity= workSpace;
        capsuleCollider.enabled = false;
        spriteRenderer.enabled = false;
        grabRigid.gravityScale = 1f;
    }

    public bool CheckIfTooFar()
    {
        if (GrabMaxLength <= Vector2.Distance(startPos, transform.position))
        {
            return true;
        }
        return false;
    }

    public void ReturnGrab()
    {
        
        trailRenderer.enabled = false;
        workSpace.Set(-flyDirection.x * flySpeed, -flyDirection.y * flySpeed);
        grabRigid.velocity = workSpace;
        CurrentVelocity = workSpace;
        grabRigid.gravityScale = 0f;
        
    }

    
    public void GrabChaseSNB()
    {
        chaseVector = GrabReturnCollider.transform.position;
        workSpace.Set(chaseVector.x - transform.position.x, chaseVector.y - transform.position.y);
        workSpace = workSpace.normalized * flySpeed;
        grabRigid.velocity = workSpace;
        transform.rotation = flyRotation;
        CurrentVelocity = workSpace;
    }

    public bool CheckIfReturned()
    {
        return isGrabReturned;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == NormalWallLayerNumber)
        {
            HitNormal = true;
            trailRenderer.enabled = false;
        }
        else if(collision.gameObject.layer == NoGrabWallLayerNumber || collision.gameObject.layer == MagmaLayerNumber)
        {
            HitNoGrab = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GrabReturn"))
        {
            isGrabReturned = true;
            DeactivateGrab();
        }
    }
}
