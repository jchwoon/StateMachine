using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _enemyStateMachine;
    protected PlayerGroundData _groundData;
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        _enemyStateMachine = enemyStateMachine;
        _groundData = _enemyStateMachine.Enemy.Data.GroundedData;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void StartAnimation(int animationHash)
    {
        _enemyStateMachine.Enemy.Animator.SetBool(animationHash, true);
    }

    protected virtual void StopAnimation(int animationHash)
    {
        _enemyStateMachine.Enemy.Animator.SetBool(animationHash, false);
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);
        Move(movementDirection);
    }

    protected void ForceMove()
    {
        _enemyStateMachine.Enemy.Controller.Move(_enemyStateMachine.Enemy.ForceReceiver.Movement * Time.deltaTime);
    }

    // 
    private Vector3 GetMovementDirection()
    {
        return (_enemyStateMachine.Target.transform.position - _enemyStateMachine.Enemy.transform.position).normalized;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        _enemyStateMachine.Enemy.Controller.Move(((direction * movementSpeed) + _enemyStateMachine.Enemy.ForceReceiver.Movement) * Time.deltaTime);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _enemyStateMachine.Enemy.transform.rotation = Quaternion.Slerp(_enemyStateMachine.Enemy.transform.rotation, targetRotation, _enemyStateMachine.RotationDamping * Time.deltaTime);
        }
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = _enemyStateMachine.MovementSpeed * _enemyStateMachine.MovementSpeedModifier;

        return movementSpeed;

    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

    //
    protected bool IsInChaseRange()
    {
        // if (stateMachine.Target.IsDead) { return false; }

        float playerDistanceSqr = (_enemyStateMachine.Target.transform.position - _enemyStateMachine.Enemy.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _enemyStateMachine.Enemy.Data.PlayerChasingRange * _enemyStateMachine.Enemy.Data.PlayerChasingRange;
    }
}
