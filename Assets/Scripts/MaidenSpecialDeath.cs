using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using UnityEngine;

public class MaidenSpecialDeath : HurtAnimationController
{
    private CorgiController _corgiController;
    [SerializeField] protected string death = "Death";
    [SerializeField] protected string deathAirborneReachedGround = "DeathAirborneReachedGround";


    protected override void Start()
    {
        base.Start();
        _corgiController = GetComponent<CorgiController>();
    }

    private void Update()
    {
        if (_corgiController.State.JustGotGrounded)
        {
            CharacterGrounded();
        }
    }

    private void CharacterGrounded()
    {
        Debug.Log("Set Trigger to just landed");
        _animator.SetTrigger(deathAirborneReachedGround);
    }

    protected override void OnDeath()
    {
        Debug.Log("Set Trigger to death airborne");
        _animator.SetTrigger(death);
    }
}