using System;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;
using UnityEngine.Serialization;

public class ComboCharacter : CharacterAbility
{
    private AnimatorStateMachine _meleeAnimatorStateMachine;

    [SerializeField] private int defaultDamageToApply = 25;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] public Collider2D hitbox;
    [SerializeField] public GameObject Hiteffect;
    [SerializeField] private DamageOnTouch _damageOnTouch;
    // private CorgiController c;

    private Collider2D enemyInRange;

    private bool isAirborne = false;

    struct Enemy
    {
        private string enemyTag;
        private int damageToApply;
    }

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        _meleeAnimatorStateMachine = GetComponent<AnimatorStateMachine>();
    }


    // Update is called once per frame
    void Update()
    {
        if (_inputManager.ShootButton.IsDown)
        {
            if (_meleeAnimatorStateMachine.CurrentState.GetType() == typeof(IdleCombatState))
            {
                HandleInput();
            }
        }
        
    }

    private void HandleInput()
    {
        isAirborne = !_controller.State.IsGrounded;
        if (isAirborne)
        {
            float verticalInput = _verticalInput;
            if (verticalInput < -_inputManager.Threshold.y)
            {
                _meleeAnimatorStateMachine.SetNextState(new AirAttackDownEntryState());
            }
            else
            {
                _meleeAnimatorStateMachine.SetNextState(new AirAttackMeleeBaseEntryState());
            }
        }
        else
        {
            _meleeAnimatorStateMachine.SetNextState(new GroundEntryState());
        }
    }

    public void PerformAttack(int attack)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                Health health = collider.GetComponent<Health>();
                if (health != null)
                {
                    int damage = CalculateDamageToApply(attack);
                    health.Damage(damage, gameObject, .2f, .1f, Vector3.zero);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemyInRange = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemyInRange = null;
        }
    }

    private int CalculateDamageToApply(int attackType)
    {
        if (attackType == 1)
        {
            //This is to make double damage for airdown attack
            return defaultDamageToApply * 2;
        }

        return defaultDamageToApply;
    }
}