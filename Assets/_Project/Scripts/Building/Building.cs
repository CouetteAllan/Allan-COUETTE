using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Building : MonoBehaviour, IBuilding
{
    
    private BuildingVisuals _visuals;
    public BuildingVisuals Visuals => _visuals;

    private float _fireHealth = 0.0f;
    public float FireHealth { get { return _fireHealth; } set { _fireHealth = value; } }


    private float _buildingHealth = 100.0f;
    public float BuildingHealth { get { return _buildingHealth; } set { _buildingHealth = value; } }

    private BaseBuildingState _currentState;
    private BaseBuildingState _previousState;
    public BaseBuildingState PreviousState => _previousState;

    private BuildingNormalState _normalState = new();
    private BuildingOnFireState _onFireState = new();
    private BuildingDestroyedState _destroyedState = new();
    private BuildingWaterState _waterState = new();

    private void Awake()
    {
        _visuals = GetComponent<BuildingVisuals>();

        _previousState = _normalState;
        _currentState = _normalState;
        _currentState.OnEnterState(this);
    }

    private void Update()
    {
        _currentState.UpdateState(this);
        
    }

    public void LightBuilding()
    {
        _fireHealth = 100.0f;
        SwitchState(_onFireState);
    }

    public void ExtinguishFire()
    {
        //Lower the fire health until 0
        if (_currentState is BuildingOnFireState)
            SwitchState(_waterState);
    }

    public void StopExtinguishFire()
    {
        if(_previousState is BuildingOnFireState)
            SwitchState(_previousState);
    }

    public bool IsBuildingDestroyed()
    {
        return _buildingHealth <= 0.0f;
    }

    public bool IsBuildingFullyHealed()
    {
        return _fireHealth <= 0.0f;
    }

    public void SwitchState(BaseBuildingState state)
    {
        _previousState = _currentState;
        _currentState = state;
        _currentState.OnEnterState(this);
    }


}
