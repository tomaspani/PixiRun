using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public Animation spikes;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("spike activado");
        spikes.Play();
    }
}
