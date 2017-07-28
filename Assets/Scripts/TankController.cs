using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

	public float projectileVelocity = 1;
	public GameObject projectilePrefab;

	private Rigidbody2D rigidbody2D;
	private List<GameObject> projectiles = new List<GameObject> ();

	public float power = 3;
	public float turnpower = 2;
	public float friction = 3;

	void Start()
	{
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}


	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (moveVertical != 0.0f) {
			if (moveVertical > 0) {
				rigidbody2D.AddForce(transform.up * power * moveVertical);
			} else {
				// Backwards is slower than forwards.
				rigidbody2D.AddForce(transform.up * (power/2) * moveVertical);
			}
			rigidbody2D.drag = friction;
		} else {
			// No gas means high drag.
			rigidbody2D.drag = friction * 4;
		}
			
		transform.Rotate(Vector3.forward * turnpower * -moveHorizontal);
	}


	void Update()
	{

		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space)) {

			GameObject bullet = (GameObject)Instantiate (projectilePrefab, transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.position, Quaternion.identity);

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos = new Vector3(mousePos.x, mousePos.y, 0);

			//Rotate the sprite to the mouse point
			Vector3 diff = mousePos - bullet.transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

			projectiles.Add (bullet);

		}

		for (int i = 0; i < projectiles.Count; i++) {

			GameObject goBullet = projectiles [i];
			if (goBullet != null) {

				//Move the sprite towards the mouse
				goBullet.transform.position += goBullet.transform.up * projectileVelocity * Time.deltaTime;


				// Destory Bullets if they get outside the screen
				Vector3 bulletScreenPos = Camera.main.WorldToScreenPoint (goBullet.transform.position);
				if (bulletScreenPos.y >= Screen.height || bulletScreenPos.y <= 0) {

					DestroyObject (goBullet);
					projectiles.Remove (goBullet);
				}
			}
		}
	}

}
