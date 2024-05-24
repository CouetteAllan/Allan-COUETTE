using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CloudMovement),typeof(CloudInputs))]
public class CloudController : MonoBehaviour
{
    private CloudMovement _movement;
    private CloudInputs _inputs;
    private CloudRaining _rain;
    private CloudAnims _anims;

    private List<IBuilding> _hoveredBuildings;


    private void Awake()
    {
        _hoveredBuildings = new List<IBuilding>();
        _movement = GetComponent<CloudMovement>();
        _inputs = GetComponent<CloudInputs>();
        _rain = GetComponent<CloudRaining>();
        _anims = GetComponent<CloudAnims>();
        BuildingWaterState.OnBuildingExtinguished += OnBuildingExtinguished;
        BuildingManager.OnSetOnFireBuilding += OnSetOnFireBuilding;
    }

    //In case a building is set on fire and the cloud is already on top of that one
    private void OnSetOnFireBuilding(Building building)
    {
        if (_hoveredBuildings.Contains(building))
        {
            _rain.DoRain(true);
            _anims.RainAnim();
            building.ExtinguishFire();
        }
    }

    //This function is here to stop the rain anim after the fire go extinct
    private void OnBuildingExtinguished(Building building)
    {
        if (_hoveredBuildings.Contains(building) && _hoveredBuildings.Count <= 1)
        {
            _rain.DoRain(false);
            _anims.FloatAnim();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.StartGame:
                GameManager.Instance.SetPlayer(this);
                break;
            case GameState.Victory:
                _rain.DoRain(false);
                _anims.VictoryAnim();
                break;
        }
    }

    private void FixedUpdate()
    {
        _movement.UpdateMovement(_inputs.Dir);
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
        BuildingWaterState.OnBuildingExtinguished -= OnBuildingExtinguished;
        BuildingManager.OnSetOnFireBuilding -= OnSetOnFireBuilding;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IBuilding building))
        {
            if (!(building.CurrentState is BuildingOnFireState))
                return;
            _rain.DoRain(true);
            _anims.RainAnim();
            building.ExtinguishFire();
            _hoveredBuildings.Add(building);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IBuilding building))
        {
            building.StopExtinguishFire();
            _hoveredBuildings.Remove(building);
            if (_hoveredBuildings.Count <= 0)
            {
                _rain.DoRain(false);
                _anims.FloatAnim();
            }
        }
    }
}
