using CodeMonkey.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    private List<IBuilding> _buildingsList = new();
    private List<IBuilding> _buildingsOnFire = new();

    public void Start()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState newState)
    {
        if (newState == GameState.StartGame)
            Init();
    }

    private void Init()
    {
        _buildingsList = this.transform.GetComponentsInChildren<IBuilding>().ToList();
        //Delay action of 2sec
        FunctionTimer.Create(() => SetRandomBuildingOnFire(), 2.0f);
    }

    private void SetRandomBuildingOnFire()
    {
        var building = GetRandomBuilding();
        building.LightBuilding();
        _buildingsOnFire.Add(building);
    }

    private IBuilding GetRandomBuilding()
    {
        int randIndex = Random.Range(0, _buildingsList.Count);
        IBuilding building = _buildingsList[randIndex];
        _buildingsList.Remove(building);

        return building;
    }
}
