// using System;
// using UnityEngine;
// using MoreMountains.Tools;
// using MoreMountains.CorgiEngine;
// using System.Collections;
//
// public class ThreeHitCombo : CharacterAbility
// {
//     public string airAttackAnimationParameter = "airAttack";
//     public string airDownAttackAnimationParameter = "airDownAttack";
//    
//     private bool isAirborne = false;
//     private bool isAttacking = false;
//     private bool canChainAttack = false;
//     [SerializeField] private string currentAttackType;
//     [SerializeField] private string idleAnimationParameter;
//
//     protected override void HandleInput()
//     {
//         isAirborne = !_controller.State.IsGrounded;
//         if (Input.GetKeyDown(KeyCode.X))
//         {
//             if (isAirborne)
//             {
//                 float verticalInput = _verticalInput;
//                 if (verticalInput < -_inputManager.Threshold.y)
//                 {
//                     PerformAirDownAttack();
//                 }
//                 else
//                 {
//                     PerformAirAttack();
//                 }
//             }
//         
//         }
//     }
//
//     private void PerformAirAttack()
//     {
//         currentAttackType = airAttackAnimationParameter;
//         PlayAttackAnimation();
//         isAttacking = true;
//     }
//
//     private void PerformAirDownAttack()
//     {
//         currentAttackType = airDownAttackAnimationParameter;
//         PlayAttackAnimation();
//         isAttacking = true;
//     }
//
//    
//     private void PlayAttackAnimation()
//     {
//         _animator.SetTrigger(currentAttackType);
//         _animator.SetBool(idleAnimationParameter, false);
//     }
//
//    
// }

using System;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using Unity;
using UnityEngine;

public class CharacterWeaponChanger : MonoBehaviour
{
    public static CharacterWeaponChanger Instance;
    [SerializeField]
    CharacterHandleWeapon _characterHandleWeapon;

    [SerializeField] private Weapon[] _weapons;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        _characterHandleWeapon = GetComponent<CharacterHandleWeapon>();
    }

    private void Update()
    {
        // if (InputManager.Instance.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        // {
        //     HandleWeaponChange(1);
        // }
    }

    public void HandleWeaponChange(int index)
    {
        _characterHandleWeapon.ChangeWeapon(_weapons[index], index.ToString());
        Debug.Log($"Changed to: {_weapons[index]}");
    }
}