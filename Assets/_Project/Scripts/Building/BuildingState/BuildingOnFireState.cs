using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOnFireState : BaseBuildingState
{
    private float _healthLoweringPerSecond = 5.0f;
    private bool _firstTreshold = false;
    private bool _secondTreshold = false;

    public override void OnEnterState(Building building)
    {
        building.Visuals.StopHealingEffect();
        //TODO: start fire
        if (building.PreviousState is BuildingWaterState)
            return;
        building.Visuals.PlayExplosionEffect();
        building.Visuals.ActivateFire(0);
        _firstTreshold = false;
        _secondTreshold = false;
    }

    public override void UpdateState(Building building)
    {
        //TODO: actual fire lowering building health
        building.BuildingHealth -= _healthLoweringPerSecond * Time.deltaTime;

        if (building.IsBuildingDestroyed())
        {
            building.SwitchState(new BuildingDestroyedState());
        }

        if (building.BuildingHealth < 33.3f && !_secondTreshold)
        {
            building.Visuals.ActivateFire(2);
            _secondTreshold = true;

        }
        else if (building.BuildingHealth < 66.6f && !_firstTreshold)
        {
            building.Visuals.ActivateFire(1);
            _firstTreshold = true;

        }
    }
}
