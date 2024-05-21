using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CloudMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void UpdateMovement(Vector3 dir)
    {
        _rb.AddForce(dir * _speed);
    }
}
