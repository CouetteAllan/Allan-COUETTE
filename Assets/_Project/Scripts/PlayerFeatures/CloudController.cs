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

    private List<IBuilding> _hoveredBuildings = new();


    private void Awake()
    {
        _movement = GetComponent<CloudMovement>();
        _inputs = GetComponent<CloudInputs>();
        _rain = GetComponent<CloudRaining>();
        _anims = GetComponent<CloudAnims>();
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
        }
    }

    private void FixedUpdate()
    {
        _movement.UpdateMovement(_inputs.Dir);
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChange;
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
