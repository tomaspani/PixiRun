using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : ItemEffect
{
    protected override IEnumerator ApplyEffect(Model M)
    {

        _isTrigger = true;
        M.SetInvertedM();
        Debug.Log("in Confusion");
        Debug.Log(_duration);
        Debug.Log(Time.time);
        yield return new WaitForSeconds(_duration);
        Debug.Log("out confusion");
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
