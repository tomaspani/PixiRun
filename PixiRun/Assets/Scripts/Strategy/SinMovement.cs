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
        float amplitude = 1.5f;
        float frequency = 1;
        Vector3 direction = new Vector3(xAxis, 0, 1);
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        Vector3 yAxis= amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * _transform.up;
        _rb.MovePosition(_transform.position + new Vector3(direction.x, yAxis.y, 1)  * (_speed * Time.fixedDeltaTime));
    }

}
