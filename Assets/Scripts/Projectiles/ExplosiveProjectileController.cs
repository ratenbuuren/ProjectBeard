using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosiveProjectileController : ProjectileController {
    public GameObject explosionPrefab;

    protected override void onHit(Collider2D other) {
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
    }
}