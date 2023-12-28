using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerGroundState
{
    private int _idleHash;
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_idleHash == 0)
        {
            _idleHash = _playerStateMachine.Player.AnimationData.IdleHash;
        }
    }

    public override void Enter()
    {
        _playerStateMachine.MovementSpeedModifier = 0;
        base.Enter();
        StartAnimation(_idleHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_idleHash);
    }

    public override void Update()
    {
        base.Update();

        if (_playerStateMachine.MoveInputValue != Vector2.zero)
        {
            OnMove();
            return;
        }
    }
}
