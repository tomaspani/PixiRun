using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMovement : IAdvance
{
    Rigidbody _rb;
    Transform _transform;
    float _speed;

    public SinMovement(Rigidbody rb, Transform transform, float speed)
    {
        _rb = rb;
        _transform = transform;
        _speed = speed;
    }

    public void Advance(float xAxis)
    {
        float amplitude = 1;
        float frequency = .5f;

        _rb.useGravity = false;
        _transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * _transform.up;
    }

}
