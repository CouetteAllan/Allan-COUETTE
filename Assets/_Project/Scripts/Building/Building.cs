using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingState
{
    Normal,
    OnFire,
    Destroyed
}

public class Building : MonoBehaviour, IBuilding
{

    private float _fireHealth = 0.0f;
    private bool _isPlayerOnTop = false;

    private BaseBuildingState _currentState;

    private BuildingNormalState _normalState = new();
    private BuildingOnFireState _onFireState = new();
    private BuildingDestroyedState _destroyedState = new();

    private void Awake()
    {
        _currentState = _normalState;
        _currentState.OnEnterState(this);
    }

    public void LightBuilding()
    {
        _currentState = _onFireState;
        _currentState.OnEnterState(this);
    }

    public void ExtinguishFire()
    {
        //Lower the fire health until 0
        _isPlayerOnTop = true;
    }

    public void StopExtinguishFire()
    {
        //Stop lowering fire health
        _isPlayerOnTop = false;
    }

}
