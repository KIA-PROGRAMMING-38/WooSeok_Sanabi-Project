using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStateMachine
{
    public TurretState CurrentState { get; private set; }

    public void Initialize(TurretState startingstate)
    {
        CurrentState= startingstate;
        CurrentState.Enter();
    }


    public void ChangeState(TurretState newState)
    {
        CurrentState.Exit(); 
        CurrentState = newState;
        CurrentState.Enter();
    }





}
