using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : BaseTank {

	public GameObject projectilePrefab;
	public float friction = 3;

	private Rigidbody2D rigidbody2D;

	void Start() {
		base.Start();
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}


	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (moveVertical != 0.0f) {
			if (moveVertical > 0) {
				rigidbody2D.AddForce(transform.up * stats.MovementSpeed * moveVertical);
			} else {
				// Backwards is slower than forwards.
				rigidbody2D.AddForce(transform.up * (stats.MovementSpeed/2) * moveVertical);
			}
			rigidbody2D.drag = friction;
		} else {
			// No gas means high drag.
			rigidbody2D.drag = friction * 4;
		}
		float movementRotationSpeed = stats.MovementRotationSpeed;
		transform.Rotate(Vector3.forward * movementRotationSpeed * -moveHorizontal);
	}


	void Update() {
		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space)) {
			GameObject bullet = (GameObject)Instantiate (projectilePrefab, transform.Find("Barrel").Find("BulletOrigin").position, Quaternion.identity);
			//Discuss this during Hackton ;)
//			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			mousePos = new Vector3(mousePos.x, mousePos.y, 0);
//
//			//Rotate the sprite to the mouse point
//			Vector3 diff = mousePos - bullet.transform.position;
//			diff.Normalize();
//			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
//			bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			ProjectileController pc = bullet.GetComponent<ProjectileController>();
			pc.Damage = stats.ProjectileDamage;
			pc.Range = stats.ProjectileRange;
			pc.Velocity = stats.ProjectileVelocity;
			bullet.transform.rotation = transform.Find ("Barrel").transform.rotation;
		}
	}
}
