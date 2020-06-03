using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MoveControllerV3 : MonoBehaviour, ITick, ITickFixed, IAwake
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
    private Rigidbody rigidbodyPlayer;

    private bool isGrounded;

    public void OnAwake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        rigidbodyPlayer.freezeRotation = true;
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

    private void Climb()
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
        Climb();

        var t =  new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);
        transform.rotation = Quaternion.Lerp(transform.rotation, t, 100 * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(jumpButton) && isGrounded)
        {
            rigidbodyPlayer.AddForce(0, jumpForce * 50, 0, ForceMode.Impulse);//number for convenience
        }
    }

    public void Tick()
    {
        Move();         //good
        Jump();         //nice
        CheckGround();  //perfect

        currentMoveSpeed = rigidbodyPlayer.velocity.magnitude; //create method with that
    }

    public float GetSpeed()
    {
        return currentMoveSpeed;
    }

    public void TickFixed()
    {
        rigidbodyPlayer.AddForce(Physics.gravity * 2,ForceMode.Impulse);

        if (isGrounded)
        {
            rigidbodyPlayer.AddForce(moveDirection.normalized * acceleration * rigidbodyPlayer.mass);
        }
        else
        {
            rigidbodyPlayer.AddForce(moveDirection.normalized * acceleration * rigidbodyPlayer.mass * 0.1f * flySpeed);
        }
        
        if (currentMoveSpeed > maxMoveSpeed)
        {
            rigidbodyPlayer.drag = (currentMoveSpeed - maxMoveSpeed) * 2;
        }

    }
}
