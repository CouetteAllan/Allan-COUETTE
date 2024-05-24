using CodeMonkey.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingManager : Singleton<BuildingManager>
{
    public static event Action OnUpdateBuildingOnFire;
    public static event Action<Building> OnSetOnFireBuilding;

    [SerializeField] private int _nextBuildingOnFireInSeconds = 6;
    [SerializeField] private Building[] _firstBuildingsOnFire = new Building[3];

    public int BuildingOnFireCount => _buildingsOnFire.Count;

    private List<Building> _buildingsList;
    private List<Building> _buildingsOnFire;

    private int _nbActiveBuilding = 7;

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
        if(_buildingsList.Count <= 0 && _buildingsOnFire.Count <= 0)
        {
            //Set game over
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
    }

    private void OnBuildingExtinguished(Building extinguishedBuilding)
    {
        _buildingsOnFire.Remove(extinguishedBuilding);
        _buildingsList.Add(extinguishedBuilding);
        OnUpdateBuildingOnFire?.Invoke();
    }

    private void OnTick(uint tick)
    {
        if(tick % (_nextBuildingOnFireInSeconds * 5) == 0 && _buildingsList.Count > 0)
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

        _nextBuildingOnFireInSeconds = Mathf.Clamp(_nextBuildingOnFireInSeconds - GameManager.Instance.CurrentLevelIndex * 2, 6, 12);

        SetBuildingActive();
        //Delay action of 5sec
        FunctionTimer.Create(() => SetFirstBuildingOnFire(), 5.0f);
    }

    private void SetBuildingActive()
    {
        _nbActiveBuilding = Mathf.Clamp(_nbActiveBuilding + GameManager.Instance.CurrentLevelIndex * 2,0,_buildingsList.Count); 
        for (int i = _nbActiveBuilding - 1; i < _buildingsList.Count; i++)
        {
            _buildingsList[i].gameObject.SetActive(false);
        }
        _buildingsList = _buildingsList.Take(_nbActiveBuilding - 1).ToList();
    }
    private void SetFirstBuildingOnFire()
    {
        int randomIndex = UnityEngine.Random.Range(0,_firstBuildingsOnFire.Length);
        Building building = _firstBuildingsOnFire[randomIndex];
        _buildingsList.Remove(building);
        building.LightBuilding();
        _buildingsOnFire.Add(building);
        OnUpdateBuildingOnFire?.Invoke();
        OnSetOnFireBuilding?.Invoke(building);
    }

    private void SetRandomBuildingOnFire()
    {
        if (_buildingsList.Count <= 0)
            return;
        var building = GetRandomBuilding();
        building.LightBuilding();
        _buildingsOnFire.Add(building);
        OnUpdateBuildingOnFire?.Invoke();
        OnSetOnFireBuilding?.Invoke(building);
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
