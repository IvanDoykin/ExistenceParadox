using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    private CharacterController _char_controller;
    private GameObject _player;
    private float _speed = 18.0f;
    private float _gravity = -19f;

    private void Start()
    {
        _char_controller = GetComponentInParent<CharacterController>();
        _player = GetComponentInParent<Person>().gameObject;
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement.y = _gravity;

        if (Input.GetKey(KeyCode.Space))
        {
            movement.y = _speed;
        }

        movement = Vector3.ClampMagnitude(movement, _speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _char_controller.Move(movement);
    }

}
