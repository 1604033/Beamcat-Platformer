using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEntryState : MeleeBaseState
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);
        //Attack
        attackIndex = 6;
        duration = 0.5f;
        animator.SetTrigger("Jump");
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
    

