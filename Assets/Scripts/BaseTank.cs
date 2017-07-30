using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTank : MonoBehaviour {

	protected TankStats stats;

	public virtual void Start() {
		stats = GetComponent<TankStats>();
		if (stats == null) {
			Debug.Log("Failed to find 'TankStats' component");
		}
	}
	
	public float GetHealth(){
		return stats.Health;
	}

	public void TakeDamage(float amount) {
		stats.Health -= amount;
	}
}
