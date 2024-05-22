using CodeMonkey.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    [SerializeField] private int _nextBuildingOnFireInSeconds = 6;

    private List<Building> _buildingsList = new();
    private List<Building> _buildingsOnFire = new();

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
    }

    private void OnBuildingExtinguished(Building extinguishedBuilding)
    {
        _buildingsOnFire.Remove(extinguishedBuilding);
        _buildingsList.Add(extinguishedBuilding);
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
    }

    private void Init()
    {
        _buildingsList = this.transform.GetComponentsInChildren<Building>().ToList();
        //Delay action of 2sec
        FunctionTimer.Create(() => SetRandomBuildingOnFire(), 2.0f);
    }

    private void SetRandomBuildingOnFire()
    {
        if (_buildingsList.Count <= 0)
            return;
        var building = GetRandomBuilding();
        building.LightBuilding();
        _buildingsOnFire.Add(building);
    }

    private Building GetRandomBuilding()
    {
        int randIndex = Random.Range(0, _buildingsList.Count);
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
