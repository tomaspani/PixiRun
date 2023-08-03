using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Model : MonoBehaviour, IObservable
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField] Rigidbody _myRb;
    public bool isG;

    IController _myController;


    #region Strategy
    public event Action OnJump = delegate { };
    public event Action SineMovement = delegate { };
    public event Action NormalMovement = delegate { };
    public event Action InvertedMovement = delegate { };

    IAdvance _normalMovement;
    IAdvance _sinMovement;
    IAdvance _invertedMovement;
    IAdvance _currentAdvance;
    #endregion

    List<IObserver> _myObservers;


    private void Awake()
    {
        _normalMovement = new NormalMovement(_myRb, this.transform, _speed);
        _sinMovement = new SinMovement(_myRb, this.transform, _speed);
        _invertedMovement = new InvertedMovement(_myRb, this.transform, _speed);
        _currentAdvance = _normalMovement;

        _myObservers = new List<IObserver>();
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

        transform.position = new Vector3(transform.position.x, 4f, transform.position.z);
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


        if (Input.GetKeyDown(KeyCode.P))
        {
            GetHit();
        }

        /*if(agarro moneda)
                PickUpCoin();*/
    }

    void GetHit()
    {
        //le saco vida
        NotifyToObservers("GetHit");
    }


    void PickUpCoin()
    {
        //sumo cant de monedas?
        NotifyToObservers("PickUpCoin");
    }

    #region Observer
    public void Subscribe(IObserver obs)
    {
        if (!_myObservers.Contains(obs))
            _myObservers.Add(obs);
    }

    public void Unsubscribe(IObserver obs)
    {
        _myObservers.Remove(obs);
    }

    public void NotifyToObservers(string action)
    {
        for (int i = _myObservers.Count - 1; i >= 0; i--)
        {
            _myObservers[i].Notify(action);
        }
    }
    #endregion
}
