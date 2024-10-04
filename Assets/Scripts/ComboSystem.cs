using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private AnimationClip[] _animationClips;
    [SerializeField] private AnimationClip _idleAnimationClip;
    [SerializeField] private float _comboWindowDuration = 1.5f;

    private UniqueQueue<ComboStates> _comboStatesQueue = new UniqueQueue<ComboStates>();
    private Animator _animator;
    private ComboStates _currentAttack = ComboStates.None;
    private bool _isAttackPlaying;
    private bool _isComboWindowOpen;

    private void Awake()
    {
        _animator = GetComponent<Animator>() ?? throw new MissingComponentException("Animator component is missing!");
         }

    private void Update()
    {
        if (InputManager.Instance.ShootButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
        {
            Debug.Log("Clicked shoot button!!");
            HandleAttackInput();
        }
    }

    private void HandleAttackInput()
    {
        if (_isAttackPlaying || _isComboWindowOpen )
        {
            EnqueueNextAttack();
            Debug.Log("There is still combo attack");
        }
        else
        {
            InitiateAttack();
        }
    }

    private void EnqueueNextAttack()
    {
        var nextAttack = GetNextAttack(_currentAttack);
        _comboStatesQueue.Enqueue(nextAttack);
    }

    private void InitiateAttack()
    {
        _currentAttack = _currentAttack == ComboStates.None ? ComboStates.Attack1 : GetNextAttack(_currentAttack);
        Debug.Log($"Attack index: {_currentAttack}");
        PlayAttackAnimation();
        StartCoroutine(ComboWindowCoroutine());
        // CharacterWeaponChanger.Instance.HandleWeaponChange(1);
    }

    private void PlayAttackAnimation()
    {
        int animationIndex = GetAnimationIndex(_currentAttack);
        StartCoroutine(AnimationTrackerCoroutine(_animationClips[animationIndex]));
    }

    private IEnumerator ComboWindowCoroutine()
    {
        Debug.Log("Combo window opened");
        _isComboWindowOpen = true;
        yield return new WaitForSeconds(_comboWindowDuration);
    
        // Add a small buffer time to allow for input after the animation ends
        float bufferTime = 0.2f;
        float bufferEndTime = Time.time + bufferTime;
    
        while (Time.time < bufferEndTime && _isAttackPlaying)
        {
            yield return null;
        }
    
        _isComboWindowOpen = false;
        Debug.Log("Combo window closed");
    }

    private IEnumerator AnimationTrackerCoroutine(AnimationClip animationClip)
    {
        _isAttackPlaying = true;
        Debug.Log($"Current anim: {animationClip.name}");
        _animator.Play(animationClip.name);
        yield return new WaitForSeconds(animationClip.length);
        _isAttackPlaying = false;
        CheckQueue();
    }

    private void CheckQueue()
    {
        if (_comboStatesQueue.Count > 0)
        {
            _currentAttack = _comboStatesQueue.Dequeue();
            PlayAttackAnimation();
            if (!_isComboWindowOpen)
            {
                StartCoroutine(ComboWindowCoroutine());
            }
        }
        else
        {
            ResetToIdle();
        }
    }

    private void ResetToIdle()
    {
        _currentAttack = ComboStates.None;
        _animator.Play(_idleAnimationClip.name);
    }

    private static ComboStates GetNextAttack(ComboStates currentAttack)
    {
        return currentAttack switch
        {
            ComboStates.None => ComboStates.Attack1,
            ComboStates.Attack1 => ComboStates.Attack2,
            ComboStates.Attack2 => ComboStates.Attack3,
            ComboStates.Attack3 => ComboStates.Attack1,
            _ => throw new ArgumentOutOfRangeException(nameof(currentAttack), $"Unexpected attack state: {currentAttack}")
        };
    }

    private static int GetAnimationIndex(ComboStates attack)
    {
        return attack switch
        {
            ComboStates.Attack1 => 0,
            ComboStates.Attack2 => 1,
            ComboStates.Attack3 => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(attack), $"Unexpected attack state: {attack}")
        };
    }
    
    
    
}

public enum ComboStates
{
    None,
    Attack1,
    Attack2,
    Attack3,
}

[Serializable]
public class UniqueQueue<T>
{
    private Queue<T> _queue = new Queue<T>();
    private HashSet<T> _set = new HashSet<T>();

    public UniqueQueue() { }

    public UniqueQueue(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Enqueue(item);
        }
    }

    public void Enqueue(T item)
    {
        if (_set.Add(item))
        {
            _queue.Enqueue(item);
        }
    }

    public T Dequeue()
    {
        var item = _queue.Dequeue();
        _set.Remove(item);
        return item;
    }

    public bool Contains(T item) => _set.Contains(item);

    public int Count => _queue.Count;

    public List<T> ToList() => _queue.ToList();
}