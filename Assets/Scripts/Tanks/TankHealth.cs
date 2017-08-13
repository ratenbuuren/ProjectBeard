using System;
using UnityEngine;

public class TankHealth : BaseTank {

	public GameObject armorPrefab;
	
	private GameObject armorChild;
	[SerializeField] private float _currentHealth;
	[SerializeField] private float _currentArmor;
	
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

	public void ChangeHealth(float amount) {
		if (amount > 0) {
			AddHealth(amount);
		} else if (amount < 0) {
			RemoveHealth(-amount);
		}
		CheckArmor();
	}
	
	public void ChangeArmor(float amount) {
		if (amount > 0) {
			AddArmor(amount);
		} else if (amount < 0) {
			DamageArmor(-amount);
		}
		CheckArmor();
	}
	
	private void AddHealth(float amount) {
		_currentHealth = Math.Min(_currentHealth + amount, stats.GetStat(StatType.MaxHealth));
	}

	private void RemoveHealth(float amount) {
		_currentHealth -= amount;
		if (_currentHealth <= 0) {
			GameManager.Instance.RemovePlayer(this.gameObject);
			Destroy(gameObject);
		}
	}

	private void AddArmor(float amount) {
		_currentArmor = Math.Max(_currentArmor + amount, 0);
	}

	private void DamageArmor(float amount) {
		_currentArmor -= amount;
	}

	private void CheckArmor() {
		if (_currentArmor > 0 && armorChild == null) {
			armorChild = Instantiate(armorPrefab);
			armorChild.name = "Armor";
			armorChild.transform.parent = gameObject.transform;
			armorChild.transform.position = gameObject.transform.position;
		} else if (_currentArmor <= 0 && armorChild != null) {
			Destroy(armorChild.gameObject);
		}
	}
}
