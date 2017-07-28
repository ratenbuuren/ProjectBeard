using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

	public float speed = 1;
	public float projectileVelocity = 1;
	public GameObject projectilePrefab;

	private Rigidbody2D rb2d;
	private List<GameObject> projectiles = new List<GameObject> ();

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		//rb2d.AddForce (movement * speed, ForceMode2D.Impulse);
		rb2d.velocity = movement * speed;
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



//	void OnTriggerEnter2D(Collider2D other) {
//		if (other.gameObject.CompareTag ("PickUp")) {
//
//			other.gameObject.SetActive (false);
//		}
//	}
}
