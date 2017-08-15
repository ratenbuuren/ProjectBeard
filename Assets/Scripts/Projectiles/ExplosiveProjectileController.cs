using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosiveProjectileController : ProjectileController {
    public GameObject explosionPrefab;

    protected override void onHit(Collider2D other) {
        float rotAngle = new RandomRotation(1).value();
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(Vector3.forward*rotAngle));
        explosion.transform.localScale = transform.localScale;
        
        ExplosionController ec = explosion.GetComponent<ExplosionController>();
        ec.Damage = this.Damage;
    }
}