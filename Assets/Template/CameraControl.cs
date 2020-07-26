using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private float distance = 2.0f;
    private float rotationY;
    Vector3 PositionCorrection(Vector3 target, Vector3 position)
    {
        RaycastHit hit;
        Debug.DrawLine(target, position, Color.blue);
        if (Physics.Linecast(target, position, out hit))
        {
            float tempDistance = Vector3.Distance(target, hit.point);
            Vector3 pos = target - (transform.rotation * Vector3.forward * tempDistance);
            position = new Vector3(pos.x, position.y, pos.z); // сдвиг позиции в точку контакта
        }
        return position;
    }

    void LateUpdate()
    {
        transform.RotateAround(player.position, Vector3.up, Input.GetAxis("Mouse X") * 2.0f);


        rotationY += Input.GetAxis("Mouse Y") * 2.0f;
        transform.localEulerAngles = new Vector3(rotationY * -1.0f, transform.localEulerAngles.y , 0.0f);

        Vector3 position = player.position - (transform.rotation * Vector3.forward * distance);
        position = position + (transform.rotation * Vector3.right * 1.0f);
        position = new Vector3(position.x, player.position.y + 1.25f, position.z); // корректировка высоты
        position = PositionCorrection(player.position, position); // находим текущую позицию, относительно игрока
        transform.position = position;
    }
}
