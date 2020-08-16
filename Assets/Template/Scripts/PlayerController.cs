using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float turnsSmoothTime = 0.2f;
    private float turnSmoothVelocity;
    [SerializeField]
    private float speedSmoothTime = 0.1f;
    private float speedSmoothVelocity;
    private float currentSpeed;

    private Transform cameraT;
    [SerializeField]
    private Transform weaponT = null;


    [SerializeField]
    private GameObject InventoryPanel = null;
    [SerializeField]
    private GameObject AimPanel = null;

    private void Start()
    {
        cameraT = Camera.main.transform;
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Deg2Rad + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetRotation, ref turnSmoothVelocity, turnsSmoothTime);
        }
        float targetSpeed = speed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

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
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryPanel.SetActive(!InventoryPanel.activeInHierarchy);
            AimPanel.SetActive(!AimPanel.activeInHierarchy);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryControl.link.EquipWeapon(weaponT, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryControl.link.EquipWeapon(weaponT, 1);
        }
    }


}
