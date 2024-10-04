using MoreMountains.CorgiEngine;
using UnityEngine;

public class HurtAnimationController : MonoBehaviour
{
    private Health _health;
    private Animator _animator;
    [SerializeField] private string hurtAnimatorParameter = "Hurt";
    [SerializeField] private string deathAnimatorParameter = "Death";

    private void Start()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _health.OnHit += OnHit;
        _health.OnDeath += OnDeath;
        
    }

    private void OnDeath()
    {
        Debug.Log("On Death called");
        _animator.SetTrigger(deathAnimatorParameter);
    }

    private void OnHit()
    {
        Debug.Log("OnHit called");
        if(_health.CurrentHealth > 0.15)
            _animator.SetTrigger(hurtAnimatorParameter);
    } 
    
}
