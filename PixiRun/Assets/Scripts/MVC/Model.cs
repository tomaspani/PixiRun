using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Model : MonoBehaviour
{
    [SerializeField] float _speed;
    Rigidbody _myRb;

    IController _myController;

    private void Start()
    {
        _myRb = GetComponent<Rigidbody>();
        _myController = new Controller(this, GetComponent<View>());

    }

    private void FixedUpdate()
    {
        //Vector3 forwardMove = transform.forward * _speed * Time.fixedDeltaTime;
        //_myRb.MovePosition(_myRb.position + forwardMove);

        _myController.OnFixedUpdate();
    }

    public void Movement(float xAxis)
    {
        Vector3 direction = new Vector3(xAxis, 0 , 1);

        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        _myRb.MovePosition(transform.position + direction * (_speed * Time.fixedDeltaTime));

    }

    private void Update()
    {
        _myController.OnUpdate();
    }

   
}
