using MoreMountains.CorgiEngine;
using UnityEngine;

public class HurtAnimationController : MonoBehaviour
{
    protected Health _health;
    protected Animator _animator;
    [SerializeField] protected string hurtAnimatorParameter = "Hurt";
    [SerializeField] protected string deathAnimatorParameter = "Death";
    protected virtual void Start()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _health.OnHit += OnHit;
        _health.OnDeath += OnDeath;
        
    }

    protected virtual void OnDeath()
    {
        
        _animator.SetTrigger(deathAnimatorParameter);
    }

    private void OnHit()
    {
        if(_health.CurrentHealth > 0.15)
            _animator.SetTrigger(hurtAnimatorParameter);
    } 
    
}
