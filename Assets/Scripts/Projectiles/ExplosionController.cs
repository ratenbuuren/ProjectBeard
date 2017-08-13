using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour {

    private float _damage;
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Tank")) {
            TankHealth tankHealth = other.gameObject.GetComponent<TankHealth>();

            float armorDmg = Math.Min(_damage, tankHealth.CurrentArmor);
            float healthDmg = _damage - armorDmg;

            tankHealth.ChangeArmor(-armorDmg);
            tankHealth.ChangeHealth(-healthDmg);
        }
    }

    public float Damage {
        get { return _damage; }
        set { _damage = value; }
    }
}