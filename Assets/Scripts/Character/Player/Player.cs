using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    private PlayerStateMachine _playerStateMachine;

    [field: Header("Reference")]
    [field: SerializeField] public PlayerSO Data {  get; set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    public Animator Animator { get { return _animator; } }
    public PlayerInput PlayerInput { get { return _playerInput; } }
    public Rigidbody Rigidbody { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    public CharacterController Controller { get; set; }

    private void Awake()
    {
        AnimationData.Initialize();
        ForceReceiver = GetComponent<ForceReceiver>();
        Rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        Controller = GetComponent<CharacterController>();
        _playerStateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerStateMachine.ChangeState(_playerStateMachine.idleState);
        _playerStateMachine.Update();
    }

    private void Update()
    {
        _playerStateMachine.HandleInput();
        _playerStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _playerStateMachine.PhysicsUpdate();
    }   
}
