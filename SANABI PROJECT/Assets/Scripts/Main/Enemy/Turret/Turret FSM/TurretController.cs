using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public TurretStateMachine StateMachine { get; private set; }
    public TurretData turretData { get; private set; }

    #region States

    public TurretPopUpState PopUpState { get; private set; }
    public TurretCooldownState CooldownState { get; private set; }  
    public TurretAimingState AimingState { get; private set; }
    public TurretShootState ShootState { get; private set; }
    public TurretExecuteHoldedState ExecuteHoldedState { get; private set; }
    public TurretDeadState DeadState { get; private set; }

    #endregion
    public Animator Animator { get; private set; }

    private void Awake()
    {
        StateMachine = new TurretStateMachine();
        turretData = GetComponent<TurretData>();
        Animator = GetComponent<Animator>();

        PopUpState = new TurretPopUpState(this, StateMachine, turretData, "popUp");
        CooldownState = new TurretCooldownState(this, StateMachine, turretData, "cooldown");
        AimingState = new TurretAimingState(this, StateMachine, turretData, "aiming");
        ShootState = new TurretShootState(this, StateMachine, turretData, "shoot");
        ExecuteHoldedState = new TurretExecuteHoldedState(this, StateMachine, turretData, "executeHolded");
        DeadState = new TurretDeadState(this, StateMachine, turretData, "dead");

    }

    private void Start()
    {
        StateMachine.Initialize(PopUpState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void ChangeToAimingState()
    {
        StateMachine.ChangeState(AimingState);
    }

}
