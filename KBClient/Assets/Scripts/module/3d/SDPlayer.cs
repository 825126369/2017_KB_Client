using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDPlayer : SDObject
{
    public enum AnimationType
    {
        NONE = 0,
        Idle = 1,
        Walk = 2,
        Run = 3,
        Jump = 4,
        Attack_1 = 5,
        Attack_2 = 6,
        Get_Hid = 7,
        Dead = 8,
    }
    public const SDObjectType type = SDObjectType.Player;
    private GameEntity player = null;
    private Animator mAnimator = null;
    private Animation mAnimation = null;
    protected override void Awake()
    {
        base.Awake();
        mAnimator = GetComponentInChildren<Animator>();
        mAnimation = GetComponentInChildren<Animation>();
        gameObject.AddComponent<RoleController>();
    }

    protected override void Start()
    {
        base.Start();
        PlayAnimation(AnimationType.Idle);
    }


    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    //------------------------------------------------------------------------------------------------------------

    public void PlayAnimator(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Idle:
                mAnimator.SetInteger("idle_action", 0);
                break;
            case AnimationType.Walk:
                mAnimator.SetInteger("idle_action", 1);
                break;
            case AnimationType.Run:
                mAnimator.SetInteger("idle_action", 2);
                break;
            case AnimationType.Jump:
                mAnimator.SetInteger("idle_action", 3);
                break;
            case AnimationType.Attack_1:
                mAnimator.SetInteger("idle_action", 4);
                break;
            case AnimationType.Attack_2:
                mAnimator.SetInteger("idle_action", 5);
                break;
            case AnimationType.Get_Hid:
                mAnimator.SetInteger("idle_action", 6);
                break;
            case AnimationType.Dead:
                mAnimator.SetInteger("idle_action", 7);
                break;
        }

    }


    public void PlayAnimation(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Idle:
                mAnimation.PlayQueued("Idle", QueueMode.PlayNow);
                break;
            case AnimationType.Walk:
                mAnimation.PlayQueued("Walk", QueueMode.PlayNow);
                break;
            case AnimationType.Run:
                mAnimation.PlayQueued("Run", QueueMode.PlayNow);
                break;
            case AnimationType.Jump:
                mAnimation.PlayQueued("Jump", QueueMode.PlayNow);
                break;
            case AnimationType.Attack_1:
                mAnimation.PlayQueued("Attack_2", QueueMode.PlayNow);
                break;
            case AnimationType.Attack_2:
                mAnimation.PlayQueued("Attack_3", QueueMode.PlayNow);
                break;
            case AnimationType.Get_Hid:
                mAnimation.PlayQueued("Get_Hit", QueueMode.PlayNow);
                break;
            case AnimationType.Dead:
                mAnimation.PlayQueued("Dead", QueueMode.PlayNow);
                break;
        }
    }
}