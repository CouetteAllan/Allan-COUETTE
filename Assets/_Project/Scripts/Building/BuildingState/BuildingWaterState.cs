using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWaterState : BaseBuildingState
{
    public override void OnEnterState(Building building)
    {
        //TODO: healing effect
    }

    public override void UpdateState(Building building)
    {
        //TODO: decrease the fire health
        building.FireHealth -= 35.0f * Time.deltaTime;
        if (building.IsBuildingFullyHealed())
            building.SwitchState(new BuildingNormalState());
    }
}
