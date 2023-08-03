using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] float _lifeTime;
    float _currentLifeTime;

    Obstacles _obs;
    List<Coin> _coins;
    Coin tempCoin;

    public Collider _coinField;

    private void Awake()
    {
        _coins = new List<Coin>();
        Debug.Log(_coins);

    }

    void Update()
    {

        _currentLifeTime -= Time.deltaTime;

        /*if (_currentLifeTime <= 0)
        {
            //"MUERO"
            PlatformFactory.Instance.ReturnPlatform(this);
        }*/

    }

    public void SpawnObstacle(Obstacles o)
    {
        int r = Random.Range(2, 5);
        _obs = o;
        _obs.transform.position = transform.GetChild(r).transform.position;
    }

    public void SpawnCoin()
    {
        for (int i = 0; i < 5; i++)
        {
            tempCoin = CoinFactory.Instance.GetObject();
            tempCoin.transform.position = GetRandomPointInCollider(_coinField);
            Debug.Log(_coins);
            _coins.Add(tempCoin);
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        return new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            0f,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlatformFactory.Instance.ReturnPlatform(this);
            ObstaclesFactory.Instance.ReturnObstacle(_obs);
            foreach (Coin c in _coins)
            {
                CoinFactory.Instance.ReturnCoin(c);
            }
            PlatformFactory.Instance.GetObject();
        }
    }

    private void Reset()
    {
        _currentLifeTime = _lifeTime;
    }


    public static void TurnOn(Platform b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Platform b)
    {
        b.gameObject.SetActive(false);
    }
}
