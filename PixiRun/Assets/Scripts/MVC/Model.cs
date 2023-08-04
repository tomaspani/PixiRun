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

    IController _myController;

    float _jumpCooldown = 1.5f;
    float _dodgeCooldown = 1.5f;
    [SerializeField]bool _canJump = true;
    [SerializeField] bool _canDodge = true;

    #region Strategy
    public event Action OnJump = delegate { };
    public event Action OnDown = delegate { };
    public event Action SineMovement = delegate { };
    public event Action NormalMovement = delegate { };
    public event Action InvertedMovement = delegate { };

    IAdvance _normalMovement;
    IAdvance _sinMovement;
    IAdvance _invertedMovement;
    IAdvance _currentAdvance;
    #endregion

    List<IObserver> _myObservers;

    ItemEffect itemEffect;

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
        if (_canJump)
        {
            _canJump = false;
            _canDodge = false;
            _myRb.AddForce(Vector3.up * _jumpForce);
            OnJump();
            StartCoroutine(JumpCooldown());
        }
    }    

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
        _canDodge = true;
    }
    
    IEnumerator DownCooldown()
    {
        yield return new WaitForSeconds(_dodgeCooldown);
        _canJump = true;
        _canDodge = true;
    }

    public void Down()
    {
        if(_canDodge)
        {
            _canJump = false;
            _canDodge = false;
            OnDown();
            StartCoroutine(DownCooldown());
        }
    }

    public void SetNormalM()
    {
        _canJump = true;
        _canDodge = true;
        _currentAdvance = _normalMovement;
        _myRb.useGravity = true;
        NormalMovement();
    }

    public void SetSineM()
    {
        _canJump = false;
        _canDodge = false;
        _currentAdvance = _sinMovement;
        transform.position = new Vector3(transform.position.x, 3f, transform.position.z);
        _myRb.useGravity = false;
        SineMovement();
    }
    
    public void SetInvertedM()
    {
        _canJump = true;
        _canDodge = true;
        _currentAdvance = _invertedMovement;
        _myRb.useGravity = true;
        InvertedMovement();
    }

    /*public void CoinMagnet(float magnetStrength)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, magnetStrength);
        Debug.LogError("aaaa");
        foreach (Collider collider in colliders)
        {
            // Check if the object has a Rigidbody and is not the player/magnet itself
            if (collider.CompareTag("Coin") && collider.attachedRigidbody != null && collider.gameObject != gameObject)
            {
                Vector3 directionToMagnet = transform.position - collider.transform.position;
                float distance = directionToMagnet.magnitude;

                // Apply force to pull the object towards the magnet
                float forceMagnitude = magnetStrength / Mathf.Max(distance, 0.1f);
                Vector3 force = directionToMagnet.normalized * forceMagnitude;
                collider.attachedRigidbody.AddForce(force);
            }
        }
    }*/

    private void Update()
    {
        _myController.OnUpdate();


        if (Input.GetKeyDown(KeyCode.P)) //SACAR ESTO
        {
            OnLose();
        }

        /*if(agarro moneda)
                PickUpCoin();*/
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += PickUpCoin;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= PickUpCoin;
    }

    void OnLose()
    {
        //le saco vida
        NotifyToObservers("OnLose");
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
