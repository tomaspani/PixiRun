using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacles
{
    public Animation spikes;
    private void OnTriggerEnter(Collider other)
    {
        spikes.Play();
    }
}
