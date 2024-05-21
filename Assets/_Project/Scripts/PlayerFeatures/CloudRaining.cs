using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudRaining : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainEffect;

    public void DoRain(bool active)
    {
        if(active)
            _rainEffect.Play();
        else
            _rainEffect.Stop();
    }
}
