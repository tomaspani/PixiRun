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

        //_rb.useGravity = false;//deberia llamarse una sola vez, y tambien falta setear la altura inicial
        //para sinmovement deberia ser y = 2 ponele y para normalmovement y = 0 xej

        Vector3 direction = new Vector3(xAxis, 0, 1);
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        //_transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * _transform.up;
        Vector3 yAxis= amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * _transform.up;
        _rb.MovePosition(_transform.position + new Vector3(direction.x, yAxis.y,1)  * (_speed * Time.fixedDeltaTime));
    }

}
