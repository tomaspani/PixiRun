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
            _m.SineMovement += v.SineMovement;
            _m.NormalMovement += v.NormalMovement;
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

        if (Input.GetKeyDown(KeyCode.F))
            _m.SetSineM();

        if (Input.GetKeyDown(KeyCode.G))
            _m.SetNormalM();
        
        if (Input.GetKeyDown(KeyCode.H))
            _m.SetInvertedM();

    }
}
