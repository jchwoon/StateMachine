using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private int _jumpHash;
    public PlayerJumpState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_jumpHash == 0)
        {
            _jumpHash = _playerStateMachine.Player.AnimationData.JumpHash;
        }
    }

    public override void Enter()
    {
        _playerStateMachine.JumpForce = _playerStateMachine.Player.Data.AirData.JumpForce;
        _playerStateMachine.Player.ForceReceiver.Jump(_playerStateMachine.JumpForce);
        base.Enter();
        StartAnimation(_jumpHash);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_jumpHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (_playerStateMachine.Player.Controller.velocity.y <= 0)
        {
            _playerStateMachine.ChangeState(_playerStateMachine.fallState);
            return;
        }
    }
}
