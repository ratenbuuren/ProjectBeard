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
			ProjectileController pc = bullet.GetComponent<ProjectileController>();
			pc.Damage = stats.ProjectileDamage;
			pc.Range = stats.ProjectileRange;
			pc.Velocity = stats.ProjectileVelocity;
			bullet.transform.localScale = Vector2.one * stats.ProjectileSize;
			bullet.transform.rotation = transform.Find ("Barrel").transform.rotation;
		}
	}
}
