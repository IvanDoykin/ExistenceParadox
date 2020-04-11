using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    
    [SerializeField] private Transform targetForCamera;
    [SerializeField] private float speedX = 360f;
    [SerializeField] private float speedY = 200f;
    [SerializeField] private float limitY = 40f;
    [SerializeField] private float minDistance = 1.5f;
    [SerializeField] private float hideDistance = 2f;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private LayerMask noPlayer;
    [SerializeField] private float offset = 3f;

    private float _maxDistance;
    private float _currentYRotation;

    private LayerMask _camOrigin;

    private Camera _mainCamera;
    private Vector3 _localPosition;    

    private Vector3 _position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }



    void Start()
    {
        _mainCamera = Camera.main;
        _localPosition = targetForCamera.InverseTransformPoint(_position);
        _maxDistance = Vector3.Distance(_position, targetForCamera.position);
        _camOrigin = _mainCamera.cullingMask;        
    }

    void LateUpdate()
    {
        _position = targetForCamera.TransformPoint(_localPosition);
        CameraRotation();
        ObstaclesReact();
        PlayerReact();
        _localPosition = targetForCamera.InverseTransformPoint(_position);
    }

    private void CameraRotation()
    {
        var rotateX = Input.GetAxis("Mouse X");
        var rotateY = Input.GetAxis("Mouse Y");

        if (rotateY != 0)
        {
            var temp = Mathf.Clamp(_currentYRotation - rotateY * speedY * Time.deltaTime, -limitY, limitY);
            if (temp != _currentYRotation)
            {
                var rot = temp - _currentYRotation;
                transform.RotateAround(targetForCamera.position, transform.right, rot);
                _currentYRotation = temp;
            }
        }
        if (rotateX != 0)
        {
            transform.RotateAround(targetForCamera.position, Vector3.up, rotateX * speedX * Time.deltaTime);
        }
        transform.LookAt(targetForCamera.position);
    }

    private void ObstaclesReact()
    {
        var distance = Vector3.Distance(_position, targetForCamera.position);
        RaycastHit hit;
        if (Physics.Raycast(targetForCamera.position, transform.position - targetForCamera.position, out hit, _maxDistance, obstacles))
        {
            _position = hit.point;
        }
        else if(distance< _maxDistance && Physics.Raycast(_position,-transform.forward, 0.1f,obstacles))
        {
            _position -= transform.forward * 0.05f;
        }

    }

    private void PlayerReact()
    {
        var distance = Vector3.Distance(_position, targetForCamera.position);
        if (distance < hideDistance)
        {
            _mainCamera.cullingMask = noPlayer;
        }
        else
            _mainCamera.cullingMask = _camOrigin;
    }
}