using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : ItemEffect
{


    /*public override void ApplyEffect(Model m)
    {
        m.SetSineM();
    }*/

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ApplyEffect(other.GetComponent<Model>()));
            GetComponentInChildren<SkinnedMeshRenderer>().gameObject.SetActive(false);
        }
    }

    protected override IEnumerator ApplyEffect(Model m)
    {
        m.SetSineM();
        yield return new WaitForSeconds(_duration);
        m.SetNormalM();
    }


}
