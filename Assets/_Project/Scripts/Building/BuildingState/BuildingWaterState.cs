using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWaterState : BaseBuildingState
{
    public static event Action<Building> OnBuildingExtinguished;

    public override void OnEnterState(Building building)
    {
        //TODO: healing effect
        building.Visuals.StartHealingBuilding();
    }

    public override void UpdateState(Building building)
    {
        //TODO: decrease the fire health
        building.FireHealth -= 35.0f * Time.deltaTime;
        if (building.IsBuildingFullyHealed())
        {
            building.SwitchState(new BuildingNormalState());
            OnBuildingExtinguished?.Invoke(building);
        }
    }
}
