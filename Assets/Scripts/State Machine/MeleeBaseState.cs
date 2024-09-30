using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class MeleeBaseState : State
{
    // How long this state should be active for before moving on
    public float duration;
    // Cached animator component
    protected Animator animator;
    protected DamageOnTouch damageOnTouch;
    // bool to check whether or not the next attack in the sequence should be played or not
    protected bool shouldCombo;
    // The attack index in the sequence of attacks
    protected int attackIndex;
    protected CharacterHandleWeapon _characterHandleWeapon;



    // The cached hit collider component of this attack
    protected Collider2D hitCollider;
    // Cached already struck objects of said attack to avoid overlapping attacks on same target
    private List<Collider2D> collidersDamaged;
    // The Hit Effect to Spawn on the afflicted Enemy
    private GameObject HitEffectPrefab;
    

    // Input buffer Timer
    private float AttackPressedTimer = 0;
    private int KeyClickCountPressedTimer = 0;

    public override void OnEnter(AnimatorStateMachine animatorStateMachine)
    {
        base.OnEnter(animatorStateMachine);
        animator = GetComponent<Animator>();
        collidersDamaged = new List<Collider2D>();
        hitCollider = GetComponent<ComboCharacter>().hitbox;
        HitEffectPrefab = GetComponent<ComboCharacter>().Hiteffect;
        damageOnTouch = GetComponent<DamageOnTouch>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;
        
        if (animator.GetFloat("Weapon.Active") > 0f)
        {
            Attack();
        }


        if (InputManager.Instance.ShootButton.IsDown)
        {
            AttackPressedTimer = 2;
            KeyClickCountPressedTimer++;
        }


        if (animator.GetFloat("AttackWindow.Open") > 0f && AttackPressedTimer > 0 && KeyClickCountPressedTimer > 1) 
        {
            shouldCombo = true; 
            KeyClickCountPressedTimer = 0;
        }
        

    }

    public override void OnExit()
    {
        base.OnExit();
    }

    protected void Attack()
    {
        // Collider2D[] collidersToDamage = new Collider2D[10];
        // ContactFilter2D filter = new ContactFilter2D();
        // filter.useTriggers = true;
        // int colliderCount = Physics2D.OverlapCollider(hitCollider, filter, collidersToDamage);
        // for (int i = 0; i < colliderCount; i++)
        // {
        //
        //     if (!collidersDamaged.Contains(collidersToDamage[i]))
        //     {
        //         TeamComponent hitTeamComponent = collidersToDamage[i].GetComponentInChildren<TeamComponent>();
        //
        //         // Only check colliders with a valid Team Componnent attached
        //         if (hitTeamComponent && hitTeamComponent.teamIndex == TeamIndex.Enemy)
        //         {
        //             GameObject.Instantiate(HitEffectPrefab, collidersToDamage[i].transform);
        //             Debug.Log("Enemy Has Taken:" + attackIndex + "Damage");
        //             collidersDamaged.Add(collidersToDamage[i]);
        //         }
        //     }
        // }
    }

}
