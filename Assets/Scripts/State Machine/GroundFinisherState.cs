using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFinisherState : MeleeBaseState
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);

        //Attack
        attackIndex = 3;
        duration = 5f;
        // animator.SetTrigger("Attack" + attackIndex);
        CharacterWeaponChanger.Instance.HandleWeaponChange(2);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
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
