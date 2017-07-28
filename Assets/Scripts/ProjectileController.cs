using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float projectileVelocity = 1;
	private float timeToLive = 5f;

	void Start()
	{
		Destroy(this.gameObject, timeToLive);
	}

	void Update () 
	{
		transform.position += transform.up * projectileVelocity * Time.deltaTime;
	}
}
