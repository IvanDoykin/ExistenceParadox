using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.forward * 1000, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward, ForceMode.Acceleration);
    }
}
