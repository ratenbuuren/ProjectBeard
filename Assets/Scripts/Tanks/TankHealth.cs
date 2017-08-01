using System;
using UnityEngine;

public class TankHealth : BaseTank {

	public GameObject armorPrefab;
	
	private GameObject armorChild;
	private float _currentHealth;
	private float _currentArmor;
	
	protected override void Start() {
		base.Start();
		_currentHealth = stats.GetStat(StatType.MaxHealth);
	}

	public float CurrentHealth {
		get { return _currentHealth; }
	}

	public float CurrentArmor {
		get { return _currentArmor; }
	}

	public void AddHealth(float amount) {
		_currentHealth = Math.Min(_currentHealth + amount, stats.GetStat(StatType.MaxHealth));
		if (_currentHealth <= 0) {
			GameManager.Instance.RemovePlayer(this.gameObject);
			Destroy(gameObject);
		}
	}
	
	public void AddArmor(float amount) {
		_currentArmor = Math.Max(_currentArmor + amount, 0);

		if (_currentArmor <= 0 && armorChild != null) {
			Destroy(armorChild.gameObject);
		}
		if (_currentArmor > 0 && armorChild == null) {
			armorChild = Instantiate(armorPrefab);
			armorChild.name = "Armor";
			armorChild.transform.parent = gameObject.transform;
			armorChild.transform.position = gameObject.transform.position;
		}
	}
}
