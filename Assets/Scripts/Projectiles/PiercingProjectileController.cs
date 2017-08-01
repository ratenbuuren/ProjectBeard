using UnityEngine;

public class PiercingProjectileController : ProjectileController {
    protected override void dealDamage(Collider2D other) {
        if (other.gameObject.name.Contains("Tank")) {
            other.gameObject.GetComponent<TankHealth>().ChangeHealth(-damage);
        }
    }
}