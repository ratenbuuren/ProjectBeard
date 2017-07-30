using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	public float damage = 40f;
	public float projectileVelocity = 1;
	private float timeToLive = 5f;

	void Start()
	{
		Destroy(gameObject, timeToLive);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name.Contains("Tank"))
		{
			other.gameObject.GetComponent<BaseTank>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}

	void Update()
	{
		transform.position += transform.up * projectileVelocity * Time.deltaTime;
	}
}
