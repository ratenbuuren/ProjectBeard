using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
	public float health = 0f;
	public float armor = 0f;
	public AmmoType ammoType = AmmoType.Default; 
	public List<PowerUpEntry> powerUpEntries = new List<PowerUpEntry>();

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Contains("Tank")) {
			other.gameObject.GetComponent<TankStats>().AddStats(powerUpEntries);
			other.gameObject.GetComponent<TankHealth>().ChangeHealth(health);
			other.gameObject.GetComponent<TankHealth>().ChangeArmor(armor);
			if (ammoType != AmmoType.Default) {
				other.gameObject.GetComponent<TankStats>().AmmoType = ammoType;
			}
			Destroy(gameObject);
		}
	}
}
