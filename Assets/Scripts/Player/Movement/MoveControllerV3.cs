using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveControllerV3 : MonoBehaviour
{
    [Range(5f, 15f)]
    [SerializeField] float maxMoveSpeed = 5f;
    [Range(15f, 100f)]
    [SerializeField] private float acceleration = 100f;

    [SerializeField] private Transform player;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [Range(10f, 35f)]
    [SerializeField] private float jumpForce = 10f;
    [Range(15f, 100f)]
    [SerializeField] private float fallMultiplier = 50f;    
    [SerializeField] private float distanceToGround = 1.2f;

    [SerializeField] private LayerMask noPlayer;

    private Vector3 moveDirection;
    private float currentMoveSpeed;

    private float horizontal;
    private float vertical;


    private float rotationY;
    private float rotationX;

    private Rigidbody rigidbody;

    private bool isGrounded;

    //test
    

    private void Awake()
    {
        if(player == null)
        {
            player = transform;
        }

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;

        
    }

    private void CheckGround()
    {
        bool result = false;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, distanceToGround, noPlayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * distanceToGround, Color.red);

        if (Mathf.Abs(vertical) > 0 || Mathf.Abs(horizontal) > 0)
        {
            player.rotation = Quaternion.Lerp(player.rotation, Quaternion.LookRotation(moveDirection), 10 * Time.deltaTime);
        }

        vertical =  Input.GetAxis("Vertical");
        horizontal =  Input.GetAxis("Horizontal");

        moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        if (Mathf.Abs(vertical) > 0 || Mathf.Abs(horizontal) > 0)
        {
            player.rotation = Quaternion.Lerp(player.rotation, Quaternion.LookRotation(moveDirection), 10 * Time.deltaTime);
        }

        if (Input.GetKeyDown(jumpButton) && isGrounded)
        {
            rigidbody.AddForce(moveDirection.x, jumpForce * 50, moveDirection.x, ForceMode.Impulse);
        }

        currentMoveSpeed = rigidbody.velocity.magnitude;

        
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (currentMoveSpeed < maxMoveSpeed) 
        {
            rigidbody.AddForce(moveDirection.normalized * acceleration * rigidbody.mass);
        }
        else
        {
            rigidbody.drag = currentMoveSpeed - maxMoveSpeed;
        }
        if (rigidbody.velocity.y > 0f)
        {
            rigidbody.AddForce(Physics.gravity);
        }
        else if(rigidbody.velocity.y > 0f && !isGrounded)
        {
            rigidbody.AddForce(Physics.gravity * fallMultiplier);
        }


    }
}
