using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITank : BaseTank
{

	private GameObject player;
	public GameObject projectilePrefab;
	private Transform barrelTransform;

	public float speed = 3f;
	public float fireRange = 4f;

	public float fireRate = 3f;
	private float nextFire;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		barrelTransform = transform.Find("Barrel");
	}

	void Update()
	{
		Vector3 playerPosition = player.transform.position;
		if (Vector3.Distance(transform.position, playerPosition) > fireRange)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
		}
		else
		{
			if (Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				GameObject bullet = Instantiate(projectilePrefab, transform.Find("Barrel").Find("BulletOrigin").position,
					transform.rotation);
				bullet.layer = LayerMask.NameToLayer("ProjectileEnemy");
				bullet.transform.rotation = barrelTransform.rotation;
			}
		}
		if (GetHealth() <= 0)
		{
			Destroy(gameObject);
		}
	}
}
