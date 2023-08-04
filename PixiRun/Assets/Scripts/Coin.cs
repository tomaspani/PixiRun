using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static void TurnOn(Coin b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Coin b)
    {
        Debug.Log("a");
        b.gameObject.SetActive(false);
    }
}
