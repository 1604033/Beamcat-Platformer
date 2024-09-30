using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComboState : MeleeBaseState
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);

        //Attack
        attackIndex = 2;
        duration = 5f;
        // animator.SetTrigger("Attack" + attackIndex);
        CharacterWeaponChanger.Instance.HandleWeaponChange(1);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                AnimatorStateMachine.SetNextState(new GroundFinisherState());
            }
            else
            {
                AnimatorStateMachine.SetNextStateToMain();
            }
        }
    }
}
