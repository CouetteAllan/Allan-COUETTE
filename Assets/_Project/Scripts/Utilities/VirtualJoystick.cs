using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour
{
    private Image _joystickVisual;

    private void Awake()
    {
        _joystickVisual = GetComponent<Image>();
    }

    private void Start()
    {
        CloudInputs.OnMovementStart += CloudInputs_OnMovementStart;
        CloudInputs.OnMovementExit += CloudInputs_OnMovementExit;

        ShowVisual(false);
    }

    private void CloudInputs_OnMovementExit()
    {
        ShowVisual(false);
    }

    private void CloudInputs_OnMovementStart()
    {
        ShowVisual(true);
    }

    private void ShowVisual(bool show)
    {
        float fadeMultiplier = show ? .8f : 0f;

        _joystickVisual.color = new Color(1, 1, 1, fadeMultiplier);
    }
}
