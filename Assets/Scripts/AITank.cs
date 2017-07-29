using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : MonoBehaviour {

	private GameObject player;
	public GameObject projectilePrefab;

	public float speed = 3f;
	public float fireRange = 4f;

	public float fireRate = 3f;
	private float nextFire;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () {
		Vector3 playerPosition = player.transform.position;
		if (Vector3.Distance (this.transform.position, playerPosition) > fireRange) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, playerPosition, step);
		} else {
			if (Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				GameObject bullet = Instantiate (projectilePrefab, transform.position, transform.rotation);

				//Rotate bullet sprite to the mouse point
				Vector3 diff = playerPosition - bullet.transform.position;
				diff.Normalize();
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			}
		}
	}
}
