using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttackDownEntryState : MeleeBaseState
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);
        //Attack
        attackIndex = 5;
        duration = 0.5f;
        animator.SetTrigger("AirAttackDown");

    }
    
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (fixedtime >= duration)
        {
            AnimatorStateMachine.SetNextStateToMain();
        }
    }
}
    

