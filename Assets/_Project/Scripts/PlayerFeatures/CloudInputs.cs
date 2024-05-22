using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CloudInputs : MonoBehaviour
{
    public static event Action OnMovementStart;
    public static event Action OnMovementExit;

    private PlayerInputActions _inputs;
    public Vector3 Dir {  get; private set; }
    private void Awake()
    {
        InitInputs();
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState state)
    {
        if(state == GameState.StartGame)
        {
            InitInputs();
        }
    }

    private void InitInputs()
    {
        _inputs = new PlayerInputActions();
        _inputs.Enable();

        _inputs.Player.Move.performed += Move_performed;
        _inputs.Player.Move.started += Move_started;
        _inputs.Player.Move.canceled += Move_canceled;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        OnMovementExit?.Invoke();
        Dir = Vector3.zero;

    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        OnMovementStart?.Invoke();
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        Dir = new Vector3(_inputs.Player.Move.ReadValue<Vector2>().x, 0, _inputs.Player.Move.ReadValue<Vector2>().y);
    }

    private void OnDestroy()
    {
        _inputs.Player.Move.performed -= Move_performed;
        _inputs.Player.Move.started -= Move_started;
        _inputs.Player.Move.canceled -= Move_canceled;
        _inputs.Disable();
        _inputs = null;

    }
}
