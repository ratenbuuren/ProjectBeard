using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : BaseTank {	
	
	public GameObject projectilePrefab;
	public float fireRangeThreshold = 4f;

	private GameObject player;
	private Transform barrelTransform;
	private float nextFire;

	protected override void Start() {
		base.Start();
		player = GameObject.FindGameObjectWithTag("Player");
		barrelTransform = transform.Find("Barrel");
	}

	void Update() {
		Vector3 playerPosition = player.transform.position;
		if (Vector3.Distance(transform.position, playerPosition) > fireRangeThreshold) {
			float step = stats.MovementSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
		}
		else {
			if (Time.time > nextFire) {
				nextFire = Time.time + stats.FireRate;
				GameObject bullet = Instantiate(projectilePrefab, transform.Find("Barrel").Find("BulletOrigin").position,
					transform.rotation);
				bullet.layer = LayerMask.NameToLayer("ProjectileEnemy");
				bullet.transform.rotation = barrelTransform.rotation;
				bullet.transform.localScale = Vector2.one * stats.ProjectileSize;
				
				ProjectileController pc = bullet.GetComponent<ProjectileController>();
				pc.Damage = stats.ProjectileDamage;
				pc.Range = stats.ProjectileRange;
				pc.Velocity = stats.ProjectileVelocity;
			}
		}
	}
}
