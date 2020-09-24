using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger == true)
            return;

        other.gameObject.GetComponent<Interactable>()?.CauseDamage(20);
        Destroy(gameObject);
    }
}

