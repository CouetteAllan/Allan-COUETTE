using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyedState : BaseBuildingState
{
    public override void OnEnterState(Building building)
    {
        //Destroy Building graph and send event
    }

    public override void UpdateState(Building building)
    {
        //Do nothing
    }
}
