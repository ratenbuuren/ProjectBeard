using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public float health = 0f;
	public float armor = 0f;
	public List<PowerUpEntry> powerUpEntries = new List<PowerUpEntry>();

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Contains("Tank")) {
			other.gameObject.GetComponent<TankStats>().AddStats(powerUpEntries);
			other.gameObject.GetComponent<TankHealth>().AddHealth(health);
			other.gameObject.GetComponent<TankHealth>().AddArmor(armor);
			Destroy(gameObject);
		}
	}
}
