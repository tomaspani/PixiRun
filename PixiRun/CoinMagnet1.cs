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
                // Check if the object is a coin and has a Rigidbody
                if (collider.CompareTag("Coin") && collider.attachedRigidbody != null)
                {
                    Vector3 directionToPlayer = playerTransform.position + new Vector3(0f,0.5f,0f) - collider.transform.position;
                    float distance = directionToPlayer.magnitude;

                    // Apply force to pull the coin towards the player
                    float forceMagnitude = magnetStrength*magnetMultiplier/ Mathf.Max(distance, 0.1f);
                    Vector3 force = directionToPlayer.normalized * forceMagnitude;
                    collider.attachedRigidbody.AddForce(force);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger object is the player
        if (other.CompareTag("Player"))
        {
            // Start the magnet effect towards the player
            isActivated = true;
            playerTransform = other.transform;

            // Disable the magnet object after it's picked up
            //gameObject.SetActive(false);

            // Start the coroutine to stop the magnet effect after the duration
            StartCoroutine(ApplyEffect(other.GetComponent<Model>()));
            Debug.LogError("yaaaaaaaaaa");
            isActivated = false;
            playerTransform = null;
        }
    }



    private void StopMagnet()
    {
        // Reset the variables and re-enable the magnet object
        Debug.Log("out Magnet");
        isActivated = false;
        playerTransform = null;
        Destroy(this, _duration);
        //gameObject.SetActive(true);
    }

    protected override IEnumerator ApplyEffect(Model M)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(_duration);
        gameObject.SetActive(false);
        Debug.Log("yolo");
        // Stop the magnet effect and reset the magnet object
        StopMagnet();
    }
}
    

