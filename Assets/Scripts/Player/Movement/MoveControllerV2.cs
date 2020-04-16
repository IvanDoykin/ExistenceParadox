using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class MoveControllerV2 : MonoBehaviour
{

	public float speed = 1.5f; // макс. скорость
	public float acceleration = 100f; // ускорение

	public Transform rotate; // объект вращения (локальный)

	public KeyCode jumpButton = KeyCode.Space; // клавиша для прыжка
	public float jumpForce = 10; // сила прыжка
	public float jumpDistance = 1.2f; // расстояние от центра объекта, до поверхности

	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;
	private float rotationY;
	private float rotationX;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		gameObject.tag = "Player";

		// объекту должен быть присвоен отдельный слой, для работы прыжка
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}

	void FixedUpdate()
	{

		// Ограничение скорости, иначе объект будет постоянно ускоряться
		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
		}
		if (Mathf.Abs(body.velocity.z) > speed)
		{
			body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
		}

		body.AddForce(direction.normalized * acceleration * body.mass * speed);
	}

	bool GetJump() // проверяем, есть ли коллайдер под ногами
	{
		bool result = false;

		RaycastHit hit;
		Ray ray = new Ray(transform.position, Vector3.down);
		if (Physics.Raycast(ray, out hit, jumpDistance, layerMask))
		{
			result = true;
		}

		return result;
	}

	void Update()
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		// вектор направления движения
		direction = new Vector3(h, 0, v);
		direction = Camera.main.transform.TransformDirection(direction);
		direction = new Vector3(direction.x, 0, direction.z);

		if (Mathf.Abs(v) > 0 || Mathf.Abs(h) > 0) // разворот тела по вектору движения
		{
			rotate.rotation = Quaternion.Lerp(rotate.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
		}

		Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // подсветка, для визуальной настройки jumpDistance

		if (Input.GetKeyDown(jumpButton) && GetJump())
		{
			body.velocity = new Vector2(0, jumpForce);
		}
	}
}