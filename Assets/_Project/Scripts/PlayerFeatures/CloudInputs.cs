using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;


public class CloudInputs : MonoBehaviour
{
    private PlayerInputActions _inputs;
    public Vector3 Dir {  get; private set; }
    private void Awake()
    {
        _inputs = new PlayerInputActions();
        _inputs.Enable();

        _inputs.Player.Move.performed += Move_performed;
    }


    private void Move_performed(InputAction.CallbackContext obj)
    {
        Dir = new Vector3(_inputs.Player.Move.ReadValue<Vector2>().x, 0, _inputs.Player.Move.ReadValue<Vector2>().y);
    }

    private void OnDisable()
    {
        _inputs.Player.Move.performed -= Move_performed;
        _inputs.Disable();

    }
}
