using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : MonoBehaviour
{
    public static CoinFactory Instance { get; private set; }

    [SerializeField] Coin _coinPrefab;
    [SerializeField] int _coinStock = 5;


    ObjectPool<Coin> _pool;

    void Start()
    {
        Instance = this;

        //Creo un nuevo pool pasandole:
        //1.- La funcion que contiene la logica de instanciar el objeto (factoryMethod)
        //2.- La funcion que contiene la logica de que hacer al pedir el objeto (turnOnCallback)
        //3.- La funcion que contiene la logica de que hacer al devolver el objeto (turnOffCallback)
        //4.- La cantidad de objetos que se crearan en un principio
        //5.- Si es dinamico o no
        _pool = new ObjectPool<Coin>(CoinCreator, Coin.TurnOn, Coin.TurnOff, _coinStock);

        //_pool = new ObjectPool<Bullet>(() => Instantiate(_bulletPrefab), (x) => x.gameObject.SetActive(true), (x) => x.gameObject.SetActive(false), 3);
    }

    //Funcion que contiene la logica de la creacion de la bala
    Coin CoinCreator()
    {
        var temp = Instantiate(_coinPrefab, transform);
        return temp;
    }

    //Funcion que va a ser llamada cuando se pida un objeto
    public Coin GetObject()
    {
        var temp = _pool.GetObject();
        return temp;
    }

    //Funcion que va a ser llamada cuando el objeto tenga que ser devuelto al Pool
    public void ReturnCoin(Coin b)
    {
        _pool.ReturnObject(b);
    }
}
