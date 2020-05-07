using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveControllerV3 : MonoBehaviour
{
    [Range(5f, 15f)]
    [SerializeField] float maxMoveSpeed = 5f;
    
    [SerializeField] private float acceleration = 10f;

    [SerializeField] private float angleClimbing = 30;

    [Range(10f, 35f)]
    [SerializeField] private float jumpForce = 15f;

    [Range(1, 25)]
    [SerializeField] private float fallMultiplier = 10;

    [Range(1, 10)]
    [SerializeField] private float flySpeed = 2;

    [SerializeField] private float distanceToGround = 1.3f;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private Transform climbPoint;

    [SerializeField] private LayerMask noPlayer;

    
    private Vector3 moveDirection;
    private float currentMoveSpeed;
    

    private float inputVertical;
    private float inputHorizontal;
    private Rigidbody rigidbody;


    private bool isGrounded;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    private void CheckGround()
    {
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

    private void ClimbHelper()
    {
        RaycastHit hit;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        Ray ray = new Ray(climbPoint.position, moveDirection);

        if (Physics.Raycast(ray, out hit, 1f, noPlayer))
        {
            
            moveDirection = new Vector3(moveDirection.x, hit.distance / 1, moveDirection.z);                        
        }
    }

    private void Move()
    {
        inputVertical = Input.GetAxis("Vertical");
        inputHorizontal = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(inputHorizontal, 0f, inputVertical);
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);
        ClimbHelper();

        var t =  new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
        transform.rotation = Quaternion.Lerp(transform.rotation, t, 100 * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jumpButton) && isGrounded)
        {
            rigidbody.AddForce(0, jumpForce * 50, 0, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        Move();
        Jump();        
        CheckGround();

        currentMoveSpeed = rigidbody.velocity.magnitude;
    }

    public float GetSpeed()
    {
        return currentMoveSpeed;
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(Physics.gravity * 2,ForceMode.Impulse);

        if (isGrounded)
        {
            rigidbody.AddForce(moveDirection.normalized * acceleration * rigidbody.mass);
        }
        else
        {
            rigidbody.AddForce(moveDirection.normalized * acceleration * rigidbody.mass * 0.1f * flySpeed);
        }
        
        if (currentMoveSpeed > maxMoveSpeed)
        {
            rigidbody.drag = (currentMoveSpeed - maxMoveSpeed) * 2;
        }

    }
}
