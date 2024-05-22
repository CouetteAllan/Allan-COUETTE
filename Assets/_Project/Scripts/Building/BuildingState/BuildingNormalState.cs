using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNormalState : BaseBuildingState
{
    public override void OnEnterState(Building building)
    {
        //Do nothing or maybe build the actual building with nice feedback
        building.Visuals.StopAllFires();
        building.Visuals.StopHealingEffect();
        building.BuildingHealth = 100.0f;
    }

    public override void OnExitState(Building building)
    {
        
    }

    public override void UpdateState(Building building)
    {
        //Movement ? anims ?
    }
}
