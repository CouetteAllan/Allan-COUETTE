using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private ParticleSystem[] _fireEffects;


    public void PlayExplosionEffect()
    {
        _explosionEffect.Play();
    }

    public void ActivateFire(int index)
    {
        index = Mathf.Clamp(index, 0, _fireEffects.Length - 1);
        for(int i = 0; i <= index; i++)
        {
            _fireEffects[i].Play();
        }
    }

    public void StopAllFires()
    {
        foreach (var fire in _fireEffects)
        {
            fire?.Stop();
        }
    }
}
