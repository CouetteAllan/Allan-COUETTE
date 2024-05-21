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

        Vector3 targetMovement = dir;

       //AnimationCurve speedcurve = _playerController.IsMoving ? _accelerationCurve : _decelerationCurve;
       //
       //_rb.velocity = new Vector2(targetMovement.x * speedcurve.Evaluate(_timerCurve) * _speed, targetMovement.y * speedcurve.Evaluate(_timerCurve) * _speed);
    }
}
