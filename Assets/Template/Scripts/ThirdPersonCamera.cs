using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 10.0f;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float dstFromTarget = 2.0f;
    [SerializeField]
    private float pitchMin = -45;
    [SerializeField]
    private float pitchMax = 85;
    [SerializeField]
    private float rotationSmoothTime = 0.12f;

    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRoration;


    private float yaw;
    private float pitch;

    // Update is called once per frame
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        currentRoration = Vector3.SmoothDamp(currentRoration, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

        transform.eulerAngles = currentRoration;
        //transform.position = position + (transform.rotation * Vector3.right * 5.0f);
        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
