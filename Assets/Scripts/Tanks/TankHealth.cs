using System;
using UnityEngine;

public class TankHealth : BaseTank {

	public GameObject armorPrefab;
	
	private GameObject armorChild;
	private float _currentHealth;
	private float _currentArmor;
	
	protected virtual void Start() {
		base.Start();
		_currentHealth = stats.GetStat(StatType.MaxHealth);
	}

	public float CurrentHealth {
		get { return _currentHealth; }
		set { _currentHealth = value; }
	}

	public float CurrentArmor {
		get { return _currentArmor; }
		set { _currentArmor = value; }
	}

	public void TakeDamage(float amount, AmmoType ammoType) {
		float armorDmg = ArmorDmg(amount, ammoType);
		float healthDmg = amount - armorDmg;
        
		AddArmor(-armorDmg);
		AddHealth(-healthDmg);
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

	private float ArmorDmg(float amount, AmmoType type) {
		float result;
		switch (type) {
			case AmmoType.ArmorPiercing:
				result = 0f;
				break;
			default:
				result = Math.Min(amount, _currentArmor);
				break;
		}
		return result;
	}
	
}
