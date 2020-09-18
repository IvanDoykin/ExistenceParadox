using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody BulletRigidbody;
    private void Start()
    {
        BulletRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        BulletRigidbody.AddForce(transform.forward * 5.0f, ForceMode.Acceleration);      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rocket"))
            return;

        Interactable interactable = other.gameObject.GetComponent<Interactable>();
        if (interactable != null)
            interactable.CauseDamage(20);

        Destroy(gameObject);
    }
}

