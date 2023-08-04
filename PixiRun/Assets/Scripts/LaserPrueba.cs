using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPrueba : MonoBehaviour
{
    [SerializeField] GameObject[] _lasers;

    void Start()
    {
        int rand = Random.Range(0, 3);
        _lasers[rand].SetActive(false);
        StartCoroutine(Lasers(rand));
    }

    IEnumerator Lasers(int n)
    {
        yield return new WaitForSeconds(2);
        _lasers[n].SetActive(true);
        int rand = Random.Range(0, 3);
        _lasers[rand].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
