using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    public BaseBuildingState CurrentState { get; }
    public void ExtinguishFire();
    public void StopExtinguishFire();
    public void LightBuilding();
}

