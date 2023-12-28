using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _inputAction;
    private PlayerInputAction.PlayerActions _playerActions;

    public PlayerInputAction.PlayerActions PlayerActions { get { return _playerActions; } }

    private void Awake()
    {
        _inputAction = new PlayerInputAction();
        _playerActions = _inputAction.Player;
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }

    private void OnDisable()
    {
        _inputAction.Disable();
    }
}
