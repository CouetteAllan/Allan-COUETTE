using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyedState : BaseBuildingState
{
    public static event Action<Building> OnBuildingDestroyed;

    public override void OnEnterState(Building building)
    {
        //Destroy Building graph and send event
        building.Visuals.StopAllFires();
        OnBuildingDestroyed?.Invoke(building);
        ScoreManagerDataHandler.AddScore(-4);
        ScoreManagerDataHandler.StopAddingScore();
    }

    public override void OnExitState(Building building)
    {
        
    }

    public override void UpdateState(Building building)
    {
        //Do nothing
    }
}
