using System;
using UnityEngine;

public class NormalProjectileController : ProjectileController {
    protected override void dealDamage(Collider2D other) {
        if (other.gameObject.name.Contains("Tank")) {
            TankHealth tankHealth = other.gameObject.GetComponent<TankHealth>();

            float armorDmg = Math.Min(damage, tankHealth.CurrentArmor);
            float healthDmg = damage - armorDmg;

            tankHealth.ChangeArmor(-armorDmg);
            tankHealth.ChangeHealth(-healthDmg);
        }
    }
}