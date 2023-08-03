using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesFactory : MonoBehaviour
{
    public static ObstaclesFactory Instance { get; private set; }

    [SerializeField] Obstacles _obstaclePrefab;
    [SerializeField] int _obstacleStock = 5;


    ObjectPool<Obstacles> _pool;

    void Start()
    {
        Instance = this;

        //Creo un nuevo pool pasandole:
        //1.- La funcion que contiene la logica de instanciar el objeto (factoryMethod)
        //2.- La funcion que contiene la logica de que hacer al pedir el objeto (turnOnCallback)
        //3.- La funcion que contiene la logica de que hacer al devolver el objeto (turnOffCallback)
        //4.- La cantidad de objetos que se crearan en un principio
        //5.- Si es dinamico o no
        _pool = new ObjectPool<Obstacles>(ObstacleCreator, Obstacles.TurnOn, Obstacles.TurnOff, _obstacleStock);

        //_pool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab), (x) => x.gameObject.SetActive(true), (x) => x.gameObject.SetActive(false), 3);
    }

    //Funcion que contiene la logica de la creacion de la bala
    Obstacles ObstacleCreator()
    {
        var temp = Instantiate(_obstaclePrefab, transform);
        return temp;
    }

    //Funcion que va a ser llamada cuando se pida un objeto
    public Obstacles GetObject()
    {
        var temp = _pool.GetObject();
        return temp;
    }

    //Funcion que va a ser llamada cuando el objeto tenga que ser devuelto al Pool
    public void ReturnPlatform(Obstacles b)
    {
        _pool.ReturnObject(b);
    }
}
