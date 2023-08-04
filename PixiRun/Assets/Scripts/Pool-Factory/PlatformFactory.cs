using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFactory : MonoBehaviour
{
    public static PlatformFactory Instance { get; private set; }

    [SerializeField] Platform _platformPrefab;
    [SerializeField] int _platformStock = 5;

    Vector3 nextSpawnTile;

    ObjectPool<Platform> _pool;

    void Start()
    {
        Instance = this;

        //Creo un nuevo pool pasandole:
        //1.- La funcion que contiene la logica de instanciar el objeto (factoryMethod)
        //2.- La funcion que contiene la logica de que hacer al pedir el objeto (turnOnCallback)
        //3.- La funcion que contiene la logica de que hacer al devolver el objeto (turnOffCallback)
        //4.- La cantidad de objetos que se crearan en un principio
        //5.- Si es dinamico o no
        _pool = new ObjectPool<Platform>(PlatformCreator, Platform.TurnOn, Platform.TurnOff, _platformStock);

        //_pool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab), (x) => x.gameObject.SetActive(true), (x) => x.gameObject.SetActive(false), 3);
    }

    //Funcion que contiene la logica de la creacion de la bala
    Platform PlatformCreator()
    {
        var tempPlatformer = Instantiate(_platformPrefab, nextSpawnTile, Quaternion.identity, transform);
        var tempObstacle = ObstaclesFactory.Instance.GetObject();
        Debug.Log(tempObstacle);
        var tempCoin = CoinFactory.Instance.GetObject();
        /*tempPlatformer.SpawnObstacle(tempObstacle);
        tempPlatformer.SpawnCoin(tempCoin);*/
        tempPlatformer.SpawnObjects(tempObstacle, tempCoin);
        nextSpawnTile = tempPlatformer.transform.GetChild(1).transform.position;
        return tempPlatformer;
    }

    //Funcion que va a ser llamada cuando se pida un objeto
    public Platform GetObject()
    {
        var tempPlatformer = _pool.GetObject();
        tempPlatformer.transform.position = nextSpawnTile;
        /*tempPlatformer.SpawnObstacle(ObstaclesFactory.Instance.GetObject());
        tempPlatformer.SpawnCoin(CoinFactory.Instance.GetObject());*/
        tempPlatformer.SpawnObjects(ObstaclesFactory.Instance.GetObject(), CoinFactory.Instance.GetObject());

        nextSpawnTile = tempPlatformer.transform.GetChild(1).transform.position;
        return tempPlatformer;
    }

    //Funcion que va a ser llamada cuando el objeto tenga que ser devuelto al Pool
    public void ReturnPlatform(Platform b)
    {
        _pool.ReturnObject(b);
    }
}
