using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private int _attackHash;
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_attackHash == 0)
        {
            _attackHash = _playerStateMachine.Player.AnimationData.AttackHash;
        }
    }

    public override void Enter()
    {
        _playerStateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(_attackHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_attackHash);
    }
}
