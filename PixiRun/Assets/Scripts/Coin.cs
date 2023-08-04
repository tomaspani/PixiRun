using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public static event Action OnCoinCollected;


    public static void TurnOn(Coin b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Coin b)
    {
        Debug.Log("a");
        b.gameObject.SetActive(false);
    }

    public void Collect()
    {
        OnCoinCollected?.Invoke();
        CoinFactory.Instance.ReturnCoin(this);
    }
}
