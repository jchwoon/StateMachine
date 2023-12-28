using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunState : PlayerGroundState
{
    private int _runHash;
    public PlayerRunState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_runHash == 0)
        {
            _runHash = _playerStateMachine.Player.AnimationData.RunHash;
        }
    }

    public override void Enter()
    {
        _playerStateMachine.MovementSpeedModifier = _groundData.RunSpeedModifier;
        base.Enter();
        StartAnimation(_runHash);
    }


    //shift�� ������ enter
    //shift�� ���� exit

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_runHash);
    }

    
}
