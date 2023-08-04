using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : ItemEffect
{
    public GameObject coinDetector;

    protected override IEnumerator ApplyEffect(Model M)
    {
        coinDetector.SetActive(true);
        yield return new WaitForSeconds(_duration);
        coinDetector.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ApplyEffect(other.GetComponent<Model>()));
            GetComponentInChildren<SkinnedMeshRenderer>().gameObject.SetActive(false);
        }
    }

    void Start()
    {
        coinDetector = GameObject.FindGameObjectWithTag("CoinDetector");
        coinDetector.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
