using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] float _lifeTime;
    float _currentLifeTime;

    Obstacles _obs;
    
    void Update()
    {

        _currentLifeTime -= Time.deltaTime;

        /*if (_currentLifeTime <= 0)
        {
            //"MUERO"
            PlatformFactory.Instance.ReturnPlatform(this);
        }*/

    }

    public void SpawnObstacle(Obstacles o)
    {
        int r = Random.Range(2, 5);
        _obs = o;
        _obs.transform.position = transform.GetChild(r).transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(this.gameObject.name);
            PlatformFactory.Instance.ReturnPlatform(this);
            ObstaclesFactory.Instance.ReturnPlatform(_obs);
            PlatformFactory.Instance.GetObject();
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
