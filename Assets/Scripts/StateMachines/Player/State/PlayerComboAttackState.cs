using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;
    private int _comboAttackHash;

    private AttackInfoData attackInfoData;
    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_comboAttackHash == 0)
        {
            _comboAttackHash = _playerStateMachine.Player.AnimationData.ComboAttackHash;
        }
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_comboAttackHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false; 

        int comboIndex = _playerStateMachine.ComboIndex;
        attackInfoData = _playerStateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        _playerStateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_comboAttackHash);

        if (!alreadyApplyCombo)
            _playerStateMachine.ComboIndex = 0;
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!_playerStateMachine.IsAttacking) return;
         
        alreadyApplyCombo = true;
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        _playerStateMachine.Player.ForceReceiver.Reset();

        _playerStateMachine.Player.ForceReceiver.AddForce(_playerStateMachine.Player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_playerStateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
                TryApplyForce();

            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }
        else
        {
            if (alreadyApplyCombo)
            {
                _playerStateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                _playerStateMachine.ChangeState(_playerStateMachine.comboAttackState);
            }
            else
            {
                _playerStateMachine.ChangeState(_playerStateMachine.idleState);
            }
        }
    }
}
