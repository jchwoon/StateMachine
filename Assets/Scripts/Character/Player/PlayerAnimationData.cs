using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _groundParameterName = "@Ground";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runParameterName = "Run";

    [SerializeField] private string _airParameterName = "@Air";
    [SerializeField] private string _jumpParameterName = "Jump";
    [SerializeField] private string _fallParameterName = "Fall";

    [SerializeField] private string _attackParameterName = "@Attack";
    [SerializeField] private string _baseAttackParameterName = "BaseAttack";
    [SerializeField] private string _comboAttackParameterName = "ComboAttack";

    public int GroundHash { get; private set; }
    public int IdleHash { get; private set; }
    public int WalkHash { get; private set; }
    public int RunHash { get; private set; }
    public int AirHash { get; private set; }
    public int JumpHash { get; private set; }
    public int FallHash { get; private set; }
    public int AttackHash { get; private set; }
    public int BaseAttackHash { get; private set; }
    public int ComboAttackHash { get; private set; }


    public void Initialize()
    {
        GroundHash = Animator.StringToHash(_groundParameterName);
        IdleHash = Animator.StringToHash(_idleParameterName);
        WalkHash = Animator.StringToHash(_walkParameterName);
        RunHash = Animator.StringToHash(_runParameterName);
        AirHash = Animator.StringToHash(_airParameterName);
        JumpHash = Animator.StringToHash(_jumpParameterName);
        FallHash = Animator.StringToHash(_fallParameterName);
        AttackHash = Animator.StringToHash(_attackParameterName);
        BaseAttackHash = Animator.StringToHash(_baseAttackParameterName);
        ComboAttackHash = Animator.StringToHash(_comboAttackParameterName);
    }
}
