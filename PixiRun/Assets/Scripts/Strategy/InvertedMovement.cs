using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedMovement : IAdvance
{
    Rigidbody _rb;
    Transform _transform;
    float _speed;

    public InvertedMovement(Rigidbody rb, Transform transform, float speed)
    {
        _rb = rb;
        _transform = transform;
        _speed = speed;
    }

    public void Advance(float xAxis)
    {
        float amplitude = 2;
        float frequency = 1;

        Vector3 direction = new Vector3(-xAxis, 0, 1);
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        Vector3 movement = amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * _transform.right;
        _rb.MovePosition(_transform.position + new Vector3(direction.x + movement.x, 0, 1) * (_speed * Time.fixedDeltaTime));
    }

}
