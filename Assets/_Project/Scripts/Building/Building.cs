using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Building : MonoBehaviour, IBuilding
{
    
    public BuildingVisuals Visuals => _visuals;
    public float FireHealth { get { return _fireHealth; } set { _fireHealth = value; } }
    public float BuildingHealth { get { return _buildingHealth; } set { _buildingHealth = value; } }


    private BuildingVisuals _visuals;

    private float _fireHealth = 0.0f;
    private float _buildingHealth = 100.0f;


    private BaseBuildingState _currentState;
    private BaseBuildingState _previousState;
    public BaseBuildingState PreviousState => _previousState;

    public BaseBuildingState CurrentState => _currentState;

    private BuildingNormalState _normalState;
    private BuildingOnFireState _onFireState;
    private BuildingDestroyedState _destroyedState;
    private BuildingWaterState _waterState;

    private bool _isDestroyed = false;

    private void Awake()
    {
        _visuals = GetComponent<BuildingVisuals>();

        _normalState = new();
        _onFireState = new();
        _destroyedState = new();
        _waterState = new();

        _previousState = _normalState;
        _currentState = _normalState;
        _currentState.OnEnterState(this);

        BuildingDestroyedState.OnBuildingDestroyed += OnBuildingDestroyed;
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState newState)
    {
        if(newState == GameState.Victory)
        {
            SwitchState(_normalState);
            Visuals.PlayVictoryFeedback();
            Destroy(this);
        }
    }

    private void OnBuildingDestroyed(Building building)
    {
        if (building != this)
            return;

        //Cannot receive any instructions and destroy colliders.
        _isDestroyed = true;
        _visuals.DestroyBuilding();
        this.GetComponent<Collider>().enabled = false;
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
        if (_isDestroyed)
            return;
        _previousState = _currentState;
        _currentState.OnExitState(this);
        _currentState = state;
        _currentState.OnEnterState(this);
    }

    private void OnDestroy()
    {
        BuildingDestroyedState.OnBuildingDestroyed -= OnBuildingDestroyed;
        GameManager.OnGameStateChange -= OnGameStateChange;
    }


}
