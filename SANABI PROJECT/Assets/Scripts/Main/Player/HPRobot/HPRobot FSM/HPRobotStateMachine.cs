using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HPRobotStateMachine
{
    public HPRobotState CurrentState { get; private set; }

    public void Initialize(HPRobotState startingState)
    {
        CurrentState= startingState;
        CurrentState.Enter();
    }

    public void ChangeState(HPRobotState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

