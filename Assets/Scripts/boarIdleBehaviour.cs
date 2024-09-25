using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class boarIdleBehaviour : StateMachineBehaviour
{
    [SerializeField] private CharacterHorizontalMovement _characterHorizontalMovement;

    private bool isIdle = true;
    
     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     Debug.Log("On state enter call!");
     _characterHorizontalMovement = animator.GetComponent<CharacterHorizontalMovement>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(_characterHorizontalMovement == null) 
            return;
        if (isIdle && _characterHorizontalMovement.MovementSpeed >= 0)
        {
            isIdle = false;
            animator.SetTrigger("walk");
        }else if (!isIdle && _characterHorizontalMovement.MovementSpeed <= 0)
        {
            animator.SetTrigger("idle");
            isIdle = true;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("walk");
        Debug.Log("On state exit call!");   

    }

}
