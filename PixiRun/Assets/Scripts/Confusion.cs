using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : ItemEffect
{
    protected override IEnumerator ApplyEffect(Model M)
    {
        M.SetInvertedM();
        yield return new WaitForSeconds(_duration);
        M.SetNormalM();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ApplyEffect(other.GetComponent<Model>()));
            GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
        }
    }
}
