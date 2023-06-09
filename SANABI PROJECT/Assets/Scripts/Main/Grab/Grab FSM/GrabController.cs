using System;
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
    public GrabExecuteHolded ExecuteHoldedState { get; private set; }
    #endregion

    #region Components
    public PlayerData playerData { get; private set; }
    public Rigidbody2D grabRigid { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public CapsuleCollider2D capsuleCollider { get; private set; }

    public PlayerArmController wireController;
    
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

    public int TurretLayerNumber { get; private set; }
    public int GrabLayerNumber { get; private set; }
    public int NormalWallLayerNumber { get; private set; }
    public int NoGrabWallLayerNumber { get; private set; }
    public int MagmaLayerNumber { get; private set; }
    
    public Vector3 startPos { get; set; }
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 workSpace;
    public Vector2 AnchorPosition { get; set; }

    #region Events
    public event Action OnGrabTurret;

    public event Action OnGrabBoss;

    public bool hasGrabbedTurret { get; set; }

    public bool hasGrabbedBoss { get; set; }    

    #endregion
    #endregion
    private void Awake()
    {
        grabStateMachine = new GrabStateMachine();
        playerData = GetComponentInParent<PlayerData>();

        IdleState = new GrabIdleState(this, grabStateMachine, playerData, "idle");
        FlyingState = new GrabFlyingState(this, grabStateMachine, playerData, "flying");
        GrabbedState = new GrabGrabbedState(this, grabStateMachine, playerData, "grabbed");
        ReturningState = new GrabReturningState(this, grabStateMachine, playerData, "returning");
        ExecuteHoldedState = new GrabExecuteHolded(this, grabStateMachine, playerData, "executeHolded");
    }
    private void Start()
    {
        grabRigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
        Animator = GetComponent<Animator>();
        playerInput = GetComponentInParent<PlayerInput>();

        TurretLayerNumber = LayerMask.NameToLayer("Turret");
        GrabLayerNumber = LayerMask.NameToLayer("Grab");
        NormalWallLayerNumber = LayerMask.NameToLayer("NormalWall");
        NoGrabWallLayerNumber = LayerMask.NameToLayer("NoGrabWall");
        MagmaLayerNumber = LayerMask.NameToLayer("Magma");

        grabStateMachine.Initialize(IdleState);
        GrabMaxLength = playerData.wireLength;
    }

    private void Update()
    {
        CurrentVelocity = grabRigid.velocity;
        grabStateMachine.grabCurrentState.LogicUpdate();
        //Debug.Log($"���� �׷����� = {grabStateMachine.grabCurrentState}");
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
        }
        else if(collision.gameObject.layer == NoGrabWallLayerNumber || collision.gameObject.layer == MagmaLayerNumber)
        {
            HitNoGrab = true;
        }
        

    }

    public void IgnoreTurretCollision(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(GrabLayerNumber, TurretLayerNumber, ignore);
    }

    private Vector3 grabbedPosition;
    public void SetGrabbedPosition(Transform enemy)
    {
        grabbedPosition= enemy.position;
    }

    public Vector3 GetGrabbedPosition()
    {
        return grabbedPosition;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GrabReturn"))
        {
            IsGrabReturned = true;
        }
        else if (collision.gameObject.CompareTag("Turret"))
        {
            SetGrabbedTurretInstanceID(collision.gameObject.GetInstanceID());
            SetGrabbedPosition(collision.transform);
            grabbedTurret = collision.gameObject;


            Physics2D.IgnoreCollision(GameManager.Instance.playerController.GetComponent<BoxCollider2D>(), GetGrabbedTurretObject().blockerCollider, true);
            hasGrabbedTurret = true;
            //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("SNB"), LayerMask.NameToLayer("SNBBlocker"), true);
            OnGrabTurret?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            SetGrabbedPosition(collision.transform);
            hasGrabbedBoss = true;

            OnGrabBoss?.Invoke();
        }
    }


    private GameObject grabbedTurret;
    public TurretController GetGrabbedTurretObject()
    {
        return grabbedTurret.GetComponent<TurretController>();
    }

    private int grabbedTurretInstanceId;

    private void SetGrabbedTurretInstanceID(int instanceID)
    {
        grabbedTurretInstanceId = instanceID;
    }
    public int GetGrabbedTurretInstanceId()
    {
        return grabbedTurretInstanceId;
    }

}
