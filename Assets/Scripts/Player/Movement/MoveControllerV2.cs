using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class MoveControllerV2 : MonoBehaviour
{

	[SerializeField] private float speed = 1.5f; // макс. скорость
	[SerializeField] private float acceleration = 100f; // ускорение

	[SerializeField] private Transform rotate;

	[SerializeField] private KeyCode jumpButton = KeyCode.Space;
	[SerializeField] private float jumpForce = 10f;
	[SerializeField] private float jumpDistance = 1.2f;
	[SerializeField] private float jumpSpeed = 5f;

	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;	
	private float fallMultiplier = 2.5f;
	private float lowJumpMultiplier = 2f;
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
		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * speed, body.velocity.y, body.velocity.z);
		}
		if (Mathf.Abs(body.velocity.z) > speed)
		{
			body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * speed);
		}

		if (GetJump())
		{
			body.AddForce(direction.normalized * acceleration * body.mass * speed);
		}
	}

	bool GetJump()
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


		direction = new Vector3(h, 0, v);
		direction = Camera.main.transform.TransformDirection(direction);
		direction = new Vector3(direction.x, 0, direction.z);

		if (Mathf.Abs(v) > 0 || Mathf.Abs(h) > 0)
		{
			rotate.rotation = Quaternion.Lerp(rotate.rotation, Quaternion.LookRotation(direction), 10 * Time.deltaTime);
		}

		Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red);

		if (Input.GetKeyDown(jumpButton) && GetJump())
		{
			body.AddForce(direction.x * jumpSpeed, jumpForce * 50, direction.z * jumpSpeed, ForceMode.Impulse);
		}

		if(body.velocity.y < 0)
		{
			body.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if(body.velocity.y > 0 && Input.GetKey(jumpButton))
		{
			body.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}
}