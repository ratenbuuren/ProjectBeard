using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float damage = 40f;
	public float projectileVelocity = 1;
	private float timeToLive = 5f;

	void Start()
	{
		Destroy(this.gameObject, timeToLive);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name.Contains("Tank"))
		{
			if(other.tag == "Player"){
				other.gameObject.GetComponent<TankController>().TakeDamage(damage);
			}
			else
			{
				other.gameObject.GetComponent<AITank>().TakeDamage(damage);
			} 
			Destroy(this.gameObject);
		}
	}

	void Update () 
	{
		transform.position += transform.up * projectileVelocity * Time.deltaTime;
	}
}
