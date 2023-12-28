using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private Player _player;

    public Player Player { get { return _player; } }
    public PlayerIdleState idleState { get; set; }
    public PlayerWalkState walkState { get; set; }
    public PlayerRunState runState { get; set; }
    public PlayerJumpState jumpState { get; set; }
    public PlayerFallState fallState { get; set; }
    public PlayerComboAttackState comboAttackState { get; }

    public float JumpForce { get; set; }
    public float MovementSpeedModifier { get; set; }
    public float MovementSpeed { get; set; }
    public Vector2 MoveInputValue { get; set; }
    public float RotationDamping { get; set; }
    public Transform MainCameraTransform { get; set; }
    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }

    public PlayerStateMachine(Player player)
    {
        _player = player;
        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        runState = new PlayerRunState(this);
        jumpState = new PlayerJumpState(this);
        fallState = new PlayerFallState(this);
        comboAttackState = new PlayerComboAttackState(this);

        MainCameraTransform = Camera.main.transform;

        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}
