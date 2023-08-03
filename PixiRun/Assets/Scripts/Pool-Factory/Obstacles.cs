using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    

    public static void TurnOn(Obstacles b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Obstacles b)
    {
        b.gameObject.SetActive(false);
    }


}
