using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] protected ObstacleTypes _type;
    public ObstacleTypes type { get { return _type; } }

    public static void TurnOn(Obstacles b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Obstacles b)
    {
        b.gameObject.SetActive(false);
    }


}

public enum ObstacleTypes
{
    Box,
    Laser,
    Fence,
    Spikes
}
