using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    public static event Action OnUpdateBuildingOnFire;

    [SerializeField] private int _nextBuildingOnFireInSeconds = 6;
    [SerializeField] private Building[] _firstBuildingsOnFire = new Building[3];

    public int BuildingOnFireCount => _buildingsOnFire.Count;

    private List<Building> _buildingsList;
    private List<Building> _buildingsOnFire;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChange += OnGameStateChange;
        TimeTickSystemDataHandler.OnTick += OnTick;
        BuildingWaterState.OnBuildingExtinguished += OnBuildingExtinguished;
        BuildingDestroyedState.OnBuildingDestroyed += OnBuildingDestroyed;
    }

    private void OnBuildingDestroyed(Building destroyedBuilding)
    {
        _buildingsOnFire.Remove(destroyedBuilding);
        OnUpdateBuildingOnFire?.Invoke();
    }

    private void OnBuildingExtinguished(Building extinguishedBuilding)
    {
        _buildingsOnFire.Remove(extinguishedBuilding);
        _buildingsList.Add(extinguishedBuilding);
        OnUpdateBuildingOnFire?.Invoke();
    }

    private void OnTick(uint tick)
    {
        if(tick % (_nextBuildingOnFireInSeconds * 5) == 0)
        {
            SetRandomBuildingOnFire();
        }
    }

    private void OnGameStateChange(GameState newState)
    {
        if (newState == GameState.StartGame)
            Init();
        else if(newState == GameState.Victory)
        {
            _buildingsOnFire.Clear();
            OnUpdateBuildingOnFire?.Invoke();
        }

    }

    private void Init()
    {
        _buildingsList = new List<Building>();
        _buildingsOnFire = new List<Building>();
        _buildingsList = this.transform.GetComponentsInChildren<Building>().ToList();
        //Delay action of 2sec
        FunctionTimer.Create(() => SetFirstBuildingOnFire(), 5.0f);
    }
    private void SetFirstBuildingOnFire()
    {
        int randomIndex = UnityEngine.Random.Range(0,_firstBuildingsOnFire.Length);
        Building building = _firstBuildingsOnFire[randomIndex];
        _buildingsList.Remove(building);
        building.LightBuilding();
        _buildingsOnFire.Add(building);
        OnUpdateBuildingOnFire?.Invoke();
    }

    private void SetRandomBuildingOnFire()
    {
        if (_buildingsList.Count <= 0)
            return;
        var building = GetRandomBuilding();
        building.LightBuilding();
        _buildingsOnFire.Add(building);
        OnUpdateBuildingOnFire?.Invoke();
    }

    private Building GetRandomBuilding()
    {
        int randIndex = UnityEngine.Random.Range(0, _buildingsList.Count);
        Building building = _buildingsList[randIndex];
        _buildingsList.Remove(building);

        return building;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
        TimeTickSystemDataHandler.OnTick -= OnTick;
        BuildingWaterState.OnBuildingExtinguished -= OnBuildingExtinguished;
        BuildingDestroyedState.OnBuildingDestroyed -= OnBuildingDestroyed;

    }
}
