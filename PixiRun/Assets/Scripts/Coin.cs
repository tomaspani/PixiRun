using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Coin : MonoBehaviour, ICollectable
{
    public static event Action OnCoinCollected;


    public static void TurnOn(Coin b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Coin b)
    {
        b.gameObject.SetActive(false);
    }

    public void Reset()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void Collect()
    {
        OnCoinCollected?.Invoke();
        CoinFactory.Instance.ReturnCoin(this);
    }
}
