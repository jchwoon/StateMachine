using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private int _baseAttackHash;
    private int _attackHash;
    private bool alreadyAppliedForce;
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        if (_attackHash == 0)
        {
            _attackHash = _enemyStateMachine.Enemy.AnimationData.AttackHash;
        }
        if (_baseAttackHash == 0)
        {
            _baseAttackHash = _enemyStateMachine.Enemy.AnimationData.BaseAttackHash;
        }
    }

    public override void Enter()
    {
        _enemyStateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(_baseAttackHash);
        StartAnimation(_attackHash);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_baseAttackHash);
        StopAnimation(_attackHash);
    }
    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizedTime = GetNormalizedTime(_enemyStateMachine.Enemy.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            if (normalizedTime >= _enemyStateMachine.Enemy.Data.ForceTransitionTime)
                TryApplyForce();
        }
        else
        {
            if (IsInChaseRange())
            {
                _enemyStateMachine.ChangeState(_enemyStateMachine.ChasingState);
                return;
            }
            else
            {
                _enemyStateMachine.ChangeState(_enemyStateMachine.IdleState);
                return;
            }
        }
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        _enemyStateMachine.Enemy.ForceReceiver.Reset();

        _enemyStateMachine.Enemy.ForceReceiver.AddForce(_enemyStateMachine.Enemy.transform.forward * _enemyStateMachine.Enemy.Data.Force);

    }
}
