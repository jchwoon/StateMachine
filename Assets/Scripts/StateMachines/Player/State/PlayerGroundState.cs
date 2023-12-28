using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_playerStateMachine.Player.AnimationData.GroundHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_playerStateMachine.Player.AnimationData.GroundHash);
    }

    public override void Update()
    {
        base.Update();

        if (_playerStateMachine.IsAttacking)
        {
            OnAttack();
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (!_playerStateMachine.Player.Controller.isGrounded && _playerStateMachine.Player.Controller.velocity.y < Physics.gravity.y * Time.fixedDeltaTime)
        {
            _playerStateMachine.ChangeState(_playerStateMachine.fallState);
            return;
        }
    }

    protected override void  OnMovementCaceled(InputAction.CallbackContext context)
    {
        if (_playerStateMachine.MoveInputValue == Vector2.zero)
        {
            return;
        }

        _playerStateMachine.ChangeState(_playerStateMachine.idleState);
        base.OnMovementCaceled(context);
    }

    protected override void OnJumpStarted(InputAction.CallbackContext context)
    {
        _playerStateMachine.ChangeState(_playerStateMachine.jumpState);
        base.OnJumpStarted(context);
    }

    protected void OnMove()
    {
        _playerStateMachine.ChangeState(_playerStateMachine.walkState);
    }

    protected virtual void OnAttack()
    {
        _playerStateMachine.ChangeState(_playerStateMachine.comboAttackState);
    }
}
