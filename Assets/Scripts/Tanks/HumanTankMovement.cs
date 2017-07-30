using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTankMovement : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;

	public float power = 3;
	public float turnpower = 2;
	public float friction = 3;

	private string horizontalAxis;
	private string verticalAxis;

	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void SetAxis(string horizontalAxis, string verticalAxis)
	{
		this.horizontalAxis = horizontalAxis;
		this.verticalAxis = verticalAxis;
	}


	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis(horizontalAxis);
		float moveVertical = Input.GetAxis(verticalAxis);

		if (moveVertical != 0.0f)
		{
			if (moveVertical > 0)
			{
				rigidbody2D.AddForce(transform.up * power * moveVertical);
			}
			else
			{
				// Backwards is slower than forwards.
				rigidbody2D.AddForce(transform.up * (power / 2) * moveVertical);
			}
			rigidbody2D.drag = friction;
		}
		else
		{
			// No gas means high drag.
			rigidbody2D.drag = friction * 4;
		}

		transform.Rotate(Vector3.forward * turnpower * -moveHorizontal);
	}
}
