using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkState : PlayerGroundState
{
    private int _walkHash;
    public PlayerWalkState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        if (_walkHash == 0)
        {
            _walkHash = _playerStateMachine.Player.AnimationData.WalkHash;
        }
        
    }

    public override void Enter()
    {
        _playerStateMachine.MovementSpeedModifier = _groundData.WalkSpeedModifier;
        base.Enter();
        StartAnimation(_walkHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_walkHash);
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        base.OnRunStarted(context);

        _playerStateMachine.ChangeState(_playerStateMachine.runState);
    }
}
