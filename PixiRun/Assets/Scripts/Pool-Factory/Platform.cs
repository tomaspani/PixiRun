using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] float _lifeTime;
    float _currentLifeTime;

    Obstacles _obs;
    [SerializeField] List<Coin> _coins;
    [SerializeField] List<ItemEffect> _itemEffects;
    Coin tempCoin;
    ItemEffect tempItem;

    public Transform _coinField;
    public Transform _itemSpawns;

    private void Awake()
    {
        _coins = new List<Coin>();

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

    public void SpawnObjects(Obstacles o, Coin c)
    {
        if(o.type == ObstacleTypes.Fence || o.type == ObstacleTypes.Laser)
        {
            _obs = o;
            _obs.transform.position = transform.GetChild(3).transform.position;
        }
        else
        {
            int rObstacle = Random.Range(2, 5);
            int rCoin = Random.Range(2, 5);
            while (rObstacle == rCoin)
            {
                rCoin = Random.Range(2, 5);
            }
            _obs = o;
            _obs.transform.position = transform.GetChild(rObstacle).transform.position;

            tempCoin = c;
            tempCoin.Reset();
            tempCoin.transform.position = new Vector3(transform.GetChild(rCoin).transform.position.x, .5f, transform.GetChild(rCoin).transform.position.z);
        }

        int rItems = Random.Range(0, 9);
        if(rItems < 1)
        {
            tempItem = Instantiate(_itemEffects[Random.Range(0,4)], transform.root);
            tempItem.transform.position = _itemSpawns.position;
        }
    }
    #region "DEPRECATED"
    /*
    public void SpawnObstacle(Obstacles o)
    {
        int r = Random.Range(2, 5);
        _obs = o;
        _obs.transform.position = transform.GetChild(r).transform.position;
    }

    public void SpawnCoin(Coin c)
    {
        int r = Random.Range(2, 5);
        tempCoin = c;
        tempCoin.gameObject.SetActive(true);
        // Debug the position before setting it
        /*Debug.Log("Before setting position: " + tempCoin.transform.position);

        tempCoin.transform.position = GetRandomPointInCollider(_coinField);

        // Debug the position after setting it
        Debug.Log("After setting position: " + tempCoin.transform.position);
        tempCoin.transform.position = new Vector3(transform.GetChild(r).transform.position.x, .5f, transform.GetChild(r).transform.position.z) ;

    }

    Vector3 GetRandomPointInCollider(Transform transform)
    {
        Collider collider = transform.GetComponent<Collider>();
        Vector3 pos = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            0f,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z));

        Debug.DrawLine(pos - Vector3.right * 0.1f, pos + Vector3.right * 0.1f, Color.red, 5f);
        Debug.DrawLine(pos - Vector3.forward * 0.1f, pos + Vector3.forward * 0.1f, Color.red, 5f);
        return pos;
    }*/
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlatformFactory.Instance.ReturnPlatform(this);
            ObstaclesFactory.Instance.ReturnObstacle(_obs);
            if (tempItem != null && !tempItem.isTrigger)
                Destroy(tempItem);
            if(tempCoin != null && tempCoin.isActiveAndEnabled)
                CoinFactory.Instance.ReturnCoin(tempCoin);
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
