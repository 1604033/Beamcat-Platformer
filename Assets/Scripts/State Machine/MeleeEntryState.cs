using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : State
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);

        State nextState = (State)new GroundEntryState();
        AnimatorStateMachine.SetNextState(nextState);
    }
}
