using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CloudMovement),typeof(CloudInputs))]
public class CloudController : MonoBehaviour
{
    private CloudMovement _movement;
    private CloudInputs _inputs;
    private CloudRaining _rain;


    private void Awake()
    {
        _movement = GetComponent<CloudMovement>();
        _inputs = GetComponent<CloudInputs>();
        _rain = GetComponent<CloudRaining>();
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
        if(other.TryGetComponent<IBuilding>(out IBuilding building))
        {
            _rain.DoRain(true);
        }
    }
}
