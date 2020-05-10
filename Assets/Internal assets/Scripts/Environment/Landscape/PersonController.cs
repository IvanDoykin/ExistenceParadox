using UnityEngine;

public class PersonController : MonoBehaviour, ITick
{
    private CharacterController characterController;
    private float speed = 18.0f;
    private float gravity = -19f;

    private void Start()
    {
        ManagerUpdate.AddTo(this);
        characterController = GetComponent<CharacterController>();
    }

    public void Tick()
    {
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
    }
}
