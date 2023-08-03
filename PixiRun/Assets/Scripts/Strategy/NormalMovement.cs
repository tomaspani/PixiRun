using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : IAdvance
{
    Rigidbody _rb;
    Transform _transform;
    float _speed;

    public NormalMovement(Rigidbody rb, Transform transform, float speed)
    {
        _rb = rb;
        _transform = transform;
        _speed = speed;
    }

    public void Advance(float xAxis)
    {
        //_rb.useGravity = true;
        Vector3 direction = new Vector3(xAxis, 0, 1);
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        _rb.MovePosition(_transform.position + direction * (_speed * Time.fixedDeltaTime));
    }
}
