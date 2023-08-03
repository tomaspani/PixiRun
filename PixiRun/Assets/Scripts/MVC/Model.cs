using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Model : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] Rigidbody _myRb;
    public bool isG;

    IController _myController;

    public event Action OnJump = delegate { };


    IAdvance _normalMovement;
    IAdvance _sinMovement;
    IAdvance _currentAdvance;

    private void Awake()
    {
        _normalMovement = new NormalMovement(_myRb, this.transform, _speed);
        _sinMovement = new SinMovement(_myRb, this.transform, _speed);
        _currentAdvance = _sinMovement;
    }

    private void Start()
    {
        _myController = new Controller(this, GetComponent<View>());

    }

    private void FixedUpdate()
    {
        _myController.OnFixedUpdate();
    }

    public void Movement(float xAxis)
    {
        //_currentAdvance = _normalMovement;

        
        _currentAdvance.Advance(xAxis);
    }
   

    void NormalMovement(float xAxis)
    {
        Vector3 direction = new Vector3(xAxis, 0, 1);
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }
        _myRb.MovePosition(transform.position + direction * (_speed * Time.fixedDeltaTime));
    }

    void SinMovement(float xAxis)
    {
        
    }

    public void Jump()
    {
        /* float height = GetComponent<Collider>().bounds.size.y;
         bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2), _groundMask);
        no anda */
        
        _myRb.AddForce(Vector3.up * _jumpForce);

        OnJump();
    }    


    private void Update()
    {
        _myController.OnUpdate();
    }

   
}
