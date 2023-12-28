using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerGroundData _groundData;
    protected PlayerAnimationData _animationData;
    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        _playerStateMachine = playerStateMachine;
        _groundData = _playerStateMachine.Player.Data.GroundData;
        _animationData = _playerStateMachine.Player.AnimationData;

    }
    public virtual void Enter()
    {
        OnEnter();
    }

    public virtual void Exit()
    {
        OnExit();
    }

    public virtual void Update()
    {
        Move();
    }
    public virtual void HandleInput()
    {
        _playerStateMachine.MoveInputValue = _playerStateMachine.Player.PlayerInput.PlayerActions.Move.ReadValue<Vector2>();
    }

    public virtual void PhysicsUpdate()
    {

    }

    private void OnEnter()
    {
        PlayerInputAction.PlayerActions playerActions = _playerStateMachine.Player.PlayerInput.PlayerActions;
        playerActions.Move.canceled += OnMovementCaceled;
        playerActions.Run.started += OnRunStarted;
        playerActions.Jump.started += OnJumpStarted;
        playerActions.Attack.performed += OnAttackPerformed;
        playerActions.Attack.canceled += OnAttackCanceled;
    }

    private void OnExit()
    {
        PlayerInputAction.PlayerActions playerActions = _playerStateMachine.Player.PlayerInput.PlayerActions;
        playerActions.Move.canceled -= OnMovementCaceled;
        playerActions.Run.started -= OnRunStarted;
        playerActions.Jump.started -= OnJumpStarted;
        playerActions.Attack.performed -= OnAttackPerformed;
        playerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void StartAnimation(int hashValue)
    {
        _playerStateMachine.Player.Animator.SetBool(hashValue, true);
    }

    protected virtual void StopAnimation(int hashValue)
    {
        _playerStateMachine.Player.Animator.SetBool(hashValue, false);
    }

    protected float GetNormalizedTime(Animator anim, string tag)
    {
        //현재 애니메이터가 동작되고 있는 State
        //내가 이제 행동할 State
        //ex) 만약 내가 idle상태에서 -> 공격을 하려 한다면 
        //Current는 Idle이되고, Next는 공격State가 된다
        AnimatorStateInfo currentInfo = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = anim.GetNextAnimatorStateInfo(0);

        if (anim.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!anim.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnMovementCaceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
        _playerStateMachine.IsAttacking = true;
    }
    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
        _playerStateMachine.IsAttacking = false;
    }

    protected void ForceMove()
    {
        _playerStateMachine.Player.Controller.Move(_playerStateMachine.Player.ForceReceiver.Movement * Time.deltaTime);
    }


    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = _playerStateMachine.MainCameraTransform.forward;
        Vector3 right = _playerStateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        return forward * _playerStateMachine.MoveInputValue.y + right * _playerStateMachine.MoveInputValue.x;
    }

    private void Move(Vector3 movementDirection)
    {
        float movementSpeed = GetMovemenetSpeed();
        _playerStateMachine.Player.Controller.Move(
            ((movementDirection * movementSpeed) + _playerStateMachine.Player.ForceReceiver.Movement) * Time.deltaTime
            );
    }

    private void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Transform playerTransform = _playerStateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, _playerStateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovemenetSpeed()
    {
        float movementSpeed = _playerStateMachine.MovementSpeed * _playerStateMachine.MovementSpeedModifier;
        return movementSpeed;
    }

    
}
