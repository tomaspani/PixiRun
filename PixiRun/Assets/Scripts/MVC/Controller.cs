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
            _m.OnJump += v.OnJump;
        }
    }

    public void OnFixedUpdate()
    {
        _m.Movement(_horizontalAxi);
    }

    public void OnUpdate()
    {
        _horizontalAxi = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            _m.Jump();
    }
}
