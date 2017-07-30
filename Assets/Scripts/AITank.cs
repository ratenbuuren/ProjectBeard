using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : BaseTank {

	
	public GameObject projectilePrefab;
	public float fireRangeThreshold = 4f;

	private GameObject player;
	private float nextFire;

	public override void Start () {
		base.Start();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () {
		Vector3 playerPosition = player.transform.position;
		if (Vector3.Distance (this.transform.position, playerPosition) > fireRangeThreshold) {
			float step = stats.MovementSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, playerPosition, step);
		} else {
			if (Time.time > nextFire) {
				nextFire = Time.time + stats.FireRate;
				GameObject bullet = Instantiate (projectilePrefab, transform.Find("Barrel").Find("BulletOrigin").position, transform.rotation);
				bullet.layer = LayerMask.NameToLayer("ProjectileEnemy");
				bullet.transform.localScale = Vector2.one * stats.ProjectileSize;
				
				ProjectileController pc = bullet.GetComponent<ProjectileController>();
				pc.Damage = stats.ProjectileDamage;
				pc.Range = stats.ProjectileRange;
				pc.Velocity = stats.ProjectileVelocity;

				//Rotate bullet sprite to the mouse point
				Vector3 diff = playerPosition - bullet.transform.position;
				diff.Normalize();
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			}
		}
		if (GetHealth () <= 0) {
			Destroy (this.gameObject);
		}
	}
}
