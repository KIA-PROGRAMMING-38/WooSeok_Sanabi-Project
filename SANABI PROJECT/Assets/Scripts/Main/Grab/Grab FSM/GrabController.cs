using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public GrabStateMachine grabStateMachine { get; private set; }
    
    #region GrabStates
    public GrabIdleState IdleState { get; private set; }
    public GrabFlyingState FlyingState { get; private set; }
    public GrabGrabbedState GrabbedState { get; private set; }
    public GrabReturningState ReturningState { get; private set; }
    #endregion

    #region Components
    public PlayerData playerData { get; private set; }
    public Rigidbody2D grabRigid { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }

    public PlayerWireController wireController;
    
    public BoxCollider2D GrabReturnCollider;
    public TrailRenderer trailRenderer { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput playerInput { get; private set; }

    public Transform playerTransform;
    #endregion

    #region Variables
    public bool HitNormal { get; set; }
    public bool HitNoGrab { get; set; }
    public float GrabMaxLength { get; private set; }
    public bool IsFlying { get; set; }
    public bool isGrappled { get; set; }
    public bool IsGrabReturned { get; set; }
    public bool isMouseInput;

    public int NormalWallLayerNumber { get; private set; }
    public int NoGrabWallLayerNumber { get; private set; }
    public int MagmaLayerNumber { get; private set; }
    
    public Vector3 startPos { get; set; }
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 workSpace;
    public Vector2 HoldPosition { get; set; }

    #endregion
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
        //Debug.Log($"현재 그랩상태 = {grabStateMachine.grabCurrentState}");
    }

    private void FixedUpdate()
    {
        grabStateMachine.grabCurrentState.PhysicsUpdate();
    }

    public void ConvertMouseInput(bool mouseInput)
    {
        if (mouseInput)
        {
            isMouseInput = true;
        }
        else
        {
            isMouseInput = false;
        }
    }
    
    public bool CheckIfMouseInput()
    {
        return isMouseInput;
    }

    public bool CheckIfTooFar()
    {
        if (GrabMaxLength <= Vector2.Distance(startPos, transform.position))
        {
            return true;
        }
        return false;
    }

    public bool CheckIfGrabReturned()
    {
        return IsGrabReturned;
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
            IsGrabReturned = true;
        }
    }
}
