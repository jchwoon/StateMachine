using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    private int _fallHash;
    public PlayerFallState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_fallHash == 0)
        {
            _fallHash = _playerStateMachine.Player.AnimationData.FallHash;
        }
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_fallHash);
    }
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_fallHash);
    }

    public override void Update()
    {
        base.Update();

        if (_playerStateMachine.Player.Controller.isGrounded)
        {
            _playerStateMachine.ChangeState(_playerStateMachine.idleState);
            return;
        }
    }
}
