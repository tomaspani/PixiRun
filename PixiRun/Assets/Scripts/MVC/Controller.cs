using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : IController
{
    Model _m;

    float _horizontalAxi;

    public Controller(Model m, View v)
    {
        _m = m;

        if (v != null)
        {
            
        }
    }

    public void OnFixedUpdate()
    {
        _m.Movement(_horizontalAxi);
    }

    public void OnUpdate()
    {
        _horizontalAxi = Input.GetAxis("Horizontal");
    }
}
