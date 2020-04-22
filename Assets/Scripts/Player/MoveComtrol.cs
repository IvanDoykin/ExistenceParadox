using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveComtrol : MonoBehaviour
{
    [SerializeField] private Transform groundChecker;

    private SphereCollider wallCheckSphereCollider;
    private Rigidbody _rigidbody;    
    private Transform _mainCamera;

    [SerializeField] private float _speed = 5f;  //скорость объекта   
    [SerializeField] private float jumpForce = 1.0f;  //высота прыжка объекта
    [SerializeField] private LayerMask noPlayer;
    private Vector3 camPos;    
    private bool _isGrounded;
    private bool _isTouchedWall;
    private float inputDelay = 0.02f;
    private float timeToJump;
    

    void Start()
    {
        _mainCamera = Camera.main.transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints =  RigidbodyConstraints.FreezeRotation;
        
    }

    private void FixedUpdate()
    {
        camPos = _mainCamera.position;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveForce = Vector3.zero;

        if (moveHorizontal != 0)
        {
            moveForce += _mainCamera.right * moveHorizontal;
        
        }

        if (moveVertical != 0)
        {
            moveForce += _mainCamera.forward * moveVertical;
        }

        if (moveForce != Vector3.zero)
        {
            _rigidbody.velocity = new Vector3(moveForce.x * _speed, _rigidbody.velocity.y, moveForce.z * _speed);
        }

        if (timeToJump>Time.time)
        {
            _rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            
        }
    }

    private void Update()
    {
        IsGroundedUpate();
        if (Input.GetButtonDown("Jump") && _isGrounded == true)
        {
            timeToJump = Time.time + inputDelay;
        }
    }

    private void IsTouchedWall()
    {
        
    }

    private void IsGroundedUpate()
    {
        if (Physics.Raycast(groundChecker.position, Vector3.down, 0.2f, noPlayer))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
}
