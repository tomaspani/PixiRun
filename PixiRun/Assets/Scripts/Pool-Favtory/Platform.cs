using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] float _lifeTime;
    float _currentLifeTime;


    void Update()
    {

        _currentLifeTime -= Time.deltaTime;

        if (_currentLifeTime <= 0)
        {
            //"MUERO"
            PlatformFactory.Instance.ReturnBullet(this);
        }

    }

    private void Reset()
    {
        _currentLifeTime = _lifeTime;
    }

    public static void TurnOn(Platform b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Platform b)
    {
        b.gameObject.SetActive(false);
    }
}
