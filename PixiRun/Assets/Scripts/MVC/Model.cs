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
    public event Action SineMovement = delegate { };
    public event Action NormalMovement = delegate { };
    public event Action InvertedMovement = delegate { };


    IAdvance _normalMovement;
    IAdvance _sinMovement;
    IAdvance _invertedMovement;
    IAdvance _currentAdvance;

    private void Awake()
    {
        _normalMovement = new NormalMovement(_myRb, this.transform, _speed);
        _sinMovement = new SinMovement(_myRb, this.transform, _speed);
        _invertedMovement = new InvertedMovement(_myRb, this.transform, _speed);
        _currentAdvance = _normalMovement;
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
        _currentAdvance.Advance(xAxis);
    }
   

    public void Jump()
    {
        /* float height = GetComponent<Collider>().bounds.size.y;
         bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2), _groundMask);
        no anda */
        
        _myRb.AddForce(Vector3.up * _jumpForce);

        OnJump();
    }    

    public void SetNormalM()
    {
        _currentAdvance = _normalMovement;
        _myRb.useGravity = true;
        NormalMovement();
    }

    public void SetSineM()
    {
        _currentAdvance = _sinMovement;
        //setear y mas alta para que parezca que si vuela!!!!!
        _myRb.useGravity = false;
        SineMovement();
    }
    
    public void SetInvertedM()
    {
        _currentAdvance = _invertedMovement;
        _myRb.useGravity = true;
        InvertedMovement();
    }



    private void Update()
    {
        _myController.OnUpdate();
    }

   
}
