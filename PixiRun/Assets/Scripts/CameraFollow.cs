using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 _offset;

    void Start()
    {
        _offset = transform.position - player.position;
    }

    void Update()
    {
        Vector3 target = player.position + _offset;
        transform.position = new Vector3(0, target.y, target.z);
    }
}
