using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

[Serializable]
public class GroundEntryState : MeleeBaseState
{
    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);
        //Attack
        attackIndex = 1;
        duration = 5f;
        // animator.SetTrigger("Attack" + attackIndex);
        CharacterWeaponChanger.Instance.HandleWeaponChange(0);
        Debug.Log("Player Attack " + attackIndex + " Fired!");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedtime >= duration)
        {
            if (shouldCombo)
            {
                AnimatorStateMachine.SetNextState(new GroundComboState());
            }
            else
            {
                AnimatorStateMachine.SetNextStateToMain();
            }
        }
    }
}
