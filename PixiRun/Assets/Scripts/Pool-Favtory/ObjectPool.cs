using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    Func<T> _factoryMethod;

    List<T> _currentStock;
    bool _isDynamic;

    Action<T> _TurnOnCallback;
    Action<T> _TurnOffCallback;

    public ObjectPool(Func<T> factoryMethod, Action<T> TurnOnCallback, Action<T> TurnOffCallback, int initialAmount, bool isDynamic = true)
    {
        //Guardo COMO se crea el objeto
        _factoryMethod = factoryMethod;

        //Guardo como inicializo el objeto al darselo al cliente
        _TurnOnCallback = TurnOnCallback;

        //Guardo como apago el objeto al regresarlo al pool
        _TurnOffCallback = TurnOffCallback;

        //Si es dinamico entonces puedo crear mas cuando mi pool este vacio
        _isDynamic = isDynamic;

        //Lista donde voy a tener los objetos apagados y disponibles para su uso
        _currentStock = new List<T>(initialAmount);

        //Creo cada uno de los objetos y los guardo en el pool apagandolos previamente
        for (int i = 0; i < initialAmount; i++)
        {
            T obj = _factoryMethod();

            _TurnOffCallback(obj);

            _currentStock.Add(obj);
        }
    }

    public T GetObject()
    {
        //Creo la variable base donde voy a guardar el objeto a devolver
        var result = default(T);

        //Si tengo objetos en la lista disponibles
        if (_currentStock.Count > 0)
        {
            //Obtengo uno (el primero)
            result = _currentStock[0];
            //Lo remuevo porque voy a usarlo
            _currentStock.RemoveAt(0);
        }
        else if (_isDynamic) //Sino pero si es dinamico
        {
            //Creo el objeto
            result = _factoryMethod();
        }

        //Lo inicializo
        _TurnOnCallback(result);

        //Devuelvo el objeto listo para que el cliente lo use
        return result;
    }

    public void ReturnObject(T obj)
    {
        //Apago el objeto para llevarlo a la lista
        _TurnOffCallback(obj);

        //Lo agrego nuevamente a la Lista
        _currentStock.Add(obj);
    }
}
