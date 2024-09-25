using System;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.Playables;

public class BossStateManager : MonoBehaviour
{
    public enum BossState
    {
        DisguisedHuman,
        Werewolf,
        FrenziedWerewolf
    }

    [System.Serializable]
    public class BossStateData
    {
        public Sprite stateSprite;
        public float moveSpeed;
        public float attackDamage;
        public float healthThreshold;
        public AnimationClip attackAnimation;
        public float attackAnimationSpeed = 1f;
    }

    [SerializeField] private BossStateData disguisedHumanStateData;
    [SerializeField] private BossStateData werewolfStateData;
    [SerializeField] private BossStateData frenziedWerewolfStateData;

    
    [SerializeField] private AudioClip bossMusic;
    [SerializeField] private AnimationClip transformationAnimation;
    [SerializeField] private Animator bossAnimator;
    
    [SerializeField] private float playerPushBackForce = 10f;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject[] cutsceneGameObjs;
    [SerializeField] private GameObject[] disabledGameobjectsDuringcutsceneGameObjs;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Health bossHealth;
    [SerializeField] private CharacterHorizontalMovement bossMovement;
    [SerializeField] public BossState currentState;
    [SerializeField] private SpriteRenderer bossSpriteRenderer;
    // private Animator bossAnimator;

    private float initialHealth;

    void Start()
    {
        // bossHealth = GetComponent<Health>();
        // bossSpriteRenderer = GetComponent<SpriteRenderer>();
        // bossMovement = GetComponent<CharacterHorizontalMovement>();
        // bossAnimator = GetComponent<Animator>();
        if (bossHealth == null || bossSpriteRenderer == null || bossMovement == null  )
        {
            Debug.LogError("Missing required components on the boss!");
            return;
        }

        initialHealth = bossHealth.MaximumHealth;
        bossHealth.OnHit += CheckHealthAndTransition;
        TransitionTo(BossState.DisguisedHuman);
    }

    void OnDisable()
    {
        if (bossHealth != null)
        {
            bossHealth.OnHit -= CheckHealthAndTransition;
        }
    }

    void CheckHealthAndTransition()
    {
        float healthPercentage = bossHealth.CurrentHealth / initialHealth;
        if (currentState == BossState.DisguisedHuman && healthPercentage <= werewolfStateData.healthThreshold)
        {
            StartCutscene();
            TransitionTo(BossState.Werewolf);

        }
        else if (currentState == BossState.Werewolf && healthPercentage <= frenziedWerewolfStateData.healthThreshold)
        {
            TransitionTo(BossState.FrenziedWerewolf);
        }
    }

    private void StartCutscene()
    {
        bossAnimator.SetTrigger("TransformWerewolfAnimation");
        _audioSource.Play();
        bossHealth.Invulnerable = true;

        // GameManager.Instance.Pause(PauseMethods.NoPauseMenu);
        // LevelManager.Instance.Players[0].Freeze();
        // LevelManager.Instance.Players[0].gameObject.SetActive(false);
        // _playableDirector.Play();
        // _playableDirector.stopped += StopCutScene();
        
    }

    public void StopCutScene()
    {
        bossHealth.Invulnerable = false;
        bossAnimator.ResetTrigger("TransformWerewolfAnimation");

        //
        // LevelManager.Instance.Players[0].UnFreeze();
        // LevelManager.Instance.Players[0].gameObject.SetActive(true);
        //
        // GameManager.Instance.UnPause();
    }

    void TransitionTo(BossState newState)
    {
        BossStateData stateData;

        switch (newState)
        {
            case BossState.DisguisedHuman:
                stateData = disguisedHumanStateData;
                break;
            case BossState.Werewolf:
                stateData = werewolfStateData;
                break;
            case BossState.FrenziedWerewolf:
                stateData = frenziedWerewolfStateData;
                break;
            default:
                return;
        }

        bossSpriteRenderer.sprite = stateData.stateSprite;
        bossMovement.MovementSpeed = stateData.moveSpeed;
        //bossAnimator.SetFloat("AttackSpeed", stateData.attackAnimationSpeed);

        if (stateData.attackAnimation != null)
        {
            //bossAnimator.SetFloat("AttackSpeed", stateData.attackAnimationSpeed);
        }

        currentState = newState;
        Debug.Log($"Boss transitioned to {newState}. Speed: {stateData.moveSpeed}, Damage: {stateData.attackDamage}");
        OnStateEnter(newState);
    }
    

    void OnStateEnter(BossState state)
    {
        switch (state)
        {
            case BossState.DisguisedHuman:
                break;
            case BossState.Werewolf:
                break;
            case BossState.FrenziedWerewolf:
                break;
        }
    }
}