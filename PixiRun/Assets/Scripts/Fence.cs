using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : Obstacles
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Model>())
            Debug.Log("toque fence");
    }
}
