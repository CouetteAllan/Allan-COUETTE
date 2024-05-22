using System;
using CodeMonkey.Utils;
using UnityEngine;

public class BuildingWaterState : BaseBuildingState
{
    public static event Action<Building> OnBuildingExtinguished;

    private bool _stopSendScore = false;
    public override void OnEnterState(Building building)
    {
        //TODO: healing effect
        building.Visuals.StartHealingBuilding();
        _stopSendScore = false;
        FunctionPeriodic.Create(() => ScoreManagerDataHandler.AddScore(1), () => _stopSendScore = true, .8f);
    }

    public override void UpdateState(Building building)
    {
        //TODO: decrease the fire health
        building.FireHealth -= 35.0f * Time.deltaTime;
        if (building.IsBuildingFullyHealed())
        {
            building.SwitchState(new BuildingNormalState());
            OnBuildingExtinguished?.Invoke(building);
            _stopSendScore = true;
            ScoreManagerDataHandler.AddScore(3);
            ScoreManagerDataHandler.StopAddingScore();
        }
    }

    public override void OnExitState(Building building)
    {
        _stopSendScore = true;
        ScoreManagerDataHandler.StopAddingScore();
    }
}
