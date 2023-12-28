using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private int _groundHash;
    private int _chasingHash;
    public EnemyChasingState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        if (_groundHash == 0)
        {
            _groundHash = _enemyStateMachine.Enemy.AnimationData.GroundHash;
        }
        if (_chasingHash == 0)
        {
            _chasingHash = enemyStateMachine.Enemy.AnimationData.RunHash;
        }
    }

    public override void Enter()
    {
        _enemyStateMachine.MovementSpeedModifier = 3.0f;
        base.Enter();
        StartAnimation(_chasingHash);
        StartAnimation(_groundHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_chasingHash);
        StopAnimation(_groundHash);
    }

    public override void Update()
    {
        base.Update();
        if (IsInAttackRange())
        {
            _enemyStateMachine.ChangeState(_enemyStateMachine.AttackState);
            return;
        }
        //else if (!IsInAttackRange())
        //{
        //    _enemyStateMachine.ChangeState(_enemyStateMachine.IdleState);
        //    return;
        //}
    }

    private bool IsInAttackRange()
    {
        float playerDistanceSqr = (_enemyStateMachine.Target.transform.position - _enemyStateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _enemyStateMachine.Enemy.Data.AttackRange * _enemyStateMachine.Enemy.Data.AttackRange;
    }
}
