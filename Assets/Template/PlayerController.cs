using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Vector3 direction;
    private Rigidbody body;
    public float acceleration = 100f;
    private void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        body.AddForce(direction.normalized * acceleration * body.mass * speed);

        if (Mathf.Abs(body.velocity.x) > speed)
        {
            body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
        }
        if (Mathf.Abs(body.velocity.z) > speed)
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
        }
    }


    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0, moveVertical);
        direction = Camera.main.transform.TransformDirection(direction);
        direction = new Vector3(direction.x, 0, direction.z);
        if (Mathf.Abs(moveVertical) > 0 || Mathf.Abs(moveHorizontal) > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
        }
    }
}
