using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuildingState 
{
    public abstract void OnEnterState(Building building);
    public abstract void UpdateState(Building building);
}
