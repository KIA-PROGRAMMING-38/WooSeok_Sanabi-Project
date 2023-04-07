using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabStateMachine 
{
    public GrabState grabCurrentState { get; private set; }

    public void Initialize(GrabState startingState)
    {
        grabCurrentState = startingState;
        grabCurrentState.Enter();
    }

    public void ChangeState(GrabState newState)
    {
        grabCurrentState.Exit();
        grabCurrentState = newState;
        grabCurrentState.Enter();
    }
}
