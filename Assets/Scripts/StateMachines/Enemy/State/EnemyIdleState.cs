using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private int _idleHash;
    private int _groundHash;
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
        if (_idleHash == 0)
        {
            _idleHash = _enemyStateMachine.Enemy.AnimationData.IdleHash;
        }
        if (_groundHash == 0)
        {
            _groundHash = _enemyStateMachine.Enemy.AnimationData.GroundHash;
        }
    }

    public override void Enter()
    {
        _enemyStateMachine.MovementSpeedModifier = 0f;
        base.Enter();
        StartAnimation(_idleHash);
        StartAnimation(_groundHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_idleHash);
        StopAnimation(_groundHash);
    }

    public override void Update()
    {
        base.Update();

        if (IsInChaseRange())
        {
            _enemyStateMachine.ChangeState(_enemyStateMachine.ChasingState);
        }
    }
}
