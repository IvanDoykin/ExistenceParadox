using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveControllerV3 : MonoBehaviour, ITick, ITickFixed
{
    //write in VK
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

                                    //why 2 empty lines?
    private Vector3 moveDirection;
    private float currentMoveSpeed;
    

    private float inputVertical;
    private float inputHorizontal;
    private Rigidbody rb;   //other name, please

                                //again 2 empty lines
    private bool isGrounded;
    //lines 9-42
    public TickData tickData { get; set; }

    private void Awake() //good - 'private' is there :)
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        ManagerUpdate.AddTo(this);
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

    private void ClimbHelper() //why 'helper'?
    {
        RaycastHit hit;
        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
        Ray ray = new Ray(climbPoint.position, moveDirection);

        if (Physics.Raycast(ray, out hit, 1f, noPlayer))
        {
                                                                //why is there empty line?
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
            rb.AddForce(0, jumpForce * 50, 0, ForceMode.Impulse); //again 'magic numbers'
        }
    }

    public void Tick()
    {
        Move();         //good
        Jump();         //nice
        CheckGround();  //perfect

        currentMoveSpeed = rb.velocity.magnitude; //create method with that
    }

    public float GetSpeed()
    {
        return currentMoveSpeed;
    }

    public void TickFixed()
    {
        rb.AddForce(Physics.gravity * 2,ForceMode.Impulse);

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * acceleration * rb.mass);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * acceleration * rb.mass * 0.1f * flySpeed);
        }
        
        if (currentMoveSpeed > maxMoveSpeed)
        {
            rb.drag = (currentMoveSpeed - maxMoveSpeed) * 2;
        }

    }
}
