using System;
using CodeMonkey.Utils;
using UnityEngine;

public class BuildingWaterState : BaseBuildingState
{
    public static event Action<Building> OnBuildingExtinguished;

    private bool _stopSendScore = false;
    private FunctionPeriodic _currentFunction = null;

     public BuildingWaterState()
    {
        _currentFunction = null;
    } 
    public override void OnEnterState(Building building)
    {
        //TODO: healing effect
        building.Visuals.StartHealingBuilding();
        _stopSendScore = false;
        if(_currentFunction == null)
        {
            _currentFunction = FunctionPeriodic.Create(() => ScoreManagerDataHandler.AddScore(1), () => _stopSendScore = true, .9f);
        }
    }

    public override void UpdateState(Building building)
    {
        //TODO: decrease the fire health
        building.FireHealth -= 35.0f * Time.deltaTime;
        if (building.IsBuildingFullyHealed())
        {
            OnBuildingExtinguished?.Invoke(building);
            _stopSendScore = true;
            ScoreManagerDataHandler.AddScore(3);
            ScoreManagerDataHandler.StopAddingScore();
            building.SwitchState(new BuildingNormalState());
        }
    }

    public override void OnExitState(Building building)
    {
        _stopSendScore = true;
        ScoreManagerDataHandler.StopAddingScore();
        _currentFunction = null;
    }
}
