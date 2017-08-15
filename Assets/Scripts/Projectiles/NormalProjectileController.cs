using System;
using UnityEngine;

public class NormalProjectileController : ProjectileController {
    protected override void onHit(Collider2D other) {
        if (other.gameObject.name.Contains("Tank")) {
            TankHealth tankHealth = other.gameObject.GetComponent<TankHealth>();

            float armorDmg = Math.Min(Damage, tankHealth.CurrentArmor);
            float healthDmg = Damage - armorDmg;

            tankHealth.ChangeArmor(-armorDmg);
            tankHealth.ChangeHealth(-healthDmg);
        }
    }
}