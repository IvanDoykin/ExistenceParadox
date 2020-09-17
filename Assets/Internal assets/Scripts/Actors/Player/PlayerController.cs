using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private float gravity = -19f;


    [SerializeField]
    private float speed = 0.0f;

    [SerializeField]
    private float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

    private Transform cameraT;
    [SerializeField]
    private WeaponController weaponController = null;
    [SerializeField]
    private QuickInventoryPanel quickInventory = null;
    [SerializeField]
    private GameObject InventoryPanel = null;
    [SerializeField]
    private GameObject AimPanel = null;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();


        cameraT = Camera.main.transform;
    }
    private void Update()
    {
        if (AimPanel.activeInHierarchy)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.Confined;


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            StopCoroutine(Example(Quaternion.identity));
            Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, cameraT.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * speed);
        }

        float targetSpeed = speed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        /*

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * currentSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * currentSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * currentSpeed * Time.deltaTime, Space.World);
        }

        */

        //temp code
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        if (Input.GetKey(KeyCode.Space))
        {
            movement.y = speed;
        }

        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        characterController.Move(movement);
        //

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
            AimPanel.SetActive(!AimPanel.activeInHierarchy);

        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            quickInventory.IndexActiveCell = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            quickInventory.IndexActiveCell = 1;
        }

        if (Input.GetMouseButton(0))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, cameraT.eulerAngles.y, transform.eulerAngles.z);
                StartCoroutine(Example(newRotation));
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponController.InputReload();
        }
    }

    private IEnumerator Example(Quaternion newRotation)
    {
        float timeCount = 0.0f;
        Quaternion oldRotation = transform.rotation;
        while (timeCount <= 1.0f)
        {
            timeCount += Time.deltaTime * speed;
            transform.rotation = Quaternion.Slerp(oldRotation, newRotation, timeCount);
            yield return new WaitForEndOfFrame();
        }
        
        weaponController.Gun();
    }

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            InventoryControl.link.AddItem(item);
        }
    }
}
