using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HPBarStateMachine
{
    public HPBarState CurrentState { get; private set; }

    public void Initialize(HPBarState startingState)
    {
        CurrentState= startingState;
        CurrentState.Enter();
    }

    public void ChangeState(HPBarState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

