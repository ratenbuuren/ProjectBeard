using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTank : MonoBehaviour
{
	public float health = 100f;

	public float GetHealth()
	{
		return health;
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		if (health <= 0)
		{
			GameManager.instance.RemovePlayer(this.gameObject);
			Destroy(gameObject);
		}
	}
}
