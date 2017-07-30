using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public List<PowerUpEntry> powerUpEntries = new List<PowerUpEntry>();

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name.Contains("Tank"))
		{
			//other.gameObject.GetComponent<BaseTank>().SendStats(powerUpEntries);
			Destroy(gameObject);
		}
	}
}
