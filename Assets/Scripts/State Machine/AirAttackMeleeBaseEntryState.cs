using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttackMeleeBaseEntryState : MeleeBaseState
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);
        //Attack
        attackIndex = 4;
        duration = 0.5f;
        animator.SetTrigger("AirAttack");
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
    

