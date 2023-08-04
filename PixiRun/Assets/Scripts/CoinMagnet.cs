using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : ItemEffect
{
    [SerializeField] private float magnetStrength = 5f;
    [SerializeField] private float magnetMultiplier = 1f;

    private bool isActivated = false;
    private Transform playerTransform;

    private void Update()
    {
        if (isActivated && playerTransform != null)
        {
            Debug.Log("in magnet");
            Debug.Log(_duration);
            Collider[] colliders = Physics.OverlapSphere(playerTransform.position, magnetStrength);

            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Coin") && collider.attachedRigidbody != null)
                {
                    Vector3 directionToPlayer = playerTransform.position + new Vector3(0f,0.5f,0f) - collider.transform.position;
                    float distance = directionToPlayer.magnitude;

                    float forceMagnitude = magnetStrength*magnetMultiplier/ Mathf.Max(distance, 0.1f);
                    Vector3 force = directionToPlayer.normalized * forceMagnitude;
                    collider.attachedRigidbody.AddForce(force);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = true;
            playerTransform = other.transform;


            StartCoroutine(ApplyEffect(other.GetComponent<Model>()));
            
        }
    }



    private void StopMagnet()
    {
        Debug.Log("out Magnet");
        isActivated = false;
        playerTransform = null;
        Destroy(this);
        //gameObject.SetActive(true);
    }

    protected override IEnumerator ApplyEffect(Model M)
    {
        _isTrigger = true;
        yield return new WaitForSeconds(_duration);
        StopMagnet();
    }
}
    

