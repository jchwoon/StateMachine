using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    private int _airHash;
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_airHash == 0)
        {
            _airHash = _playerStateMachine.Player.AnimationData.AirHash;
        }
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_airHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_airHash);
    }
}
