using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : MonoBehaviour {
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _armor = 0f;
    [SerializeField] private float _fireRate = 2f; // shots per second
    [SerializeField] private float _projectileDamage = 40f;
    [SerializeField] private float _projectileSize = 1f;
    [SerializeField] private float _projectileVelocity = 1f;
    [SerializeField] private float _projectileRange = 5f;
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _movementRotationSpeed = 2f;
    [SerializeField] private float _turretRotationSpeed = 2f;

    public void TakeDamage(float amount) {
        _health -= amount;
        if (_health <= 0) {
            GameManager.instance.RemovePlayer(this.gameObject);
            Destroy(gameObject);
        }
    }
    
    public float Health {
        get { return _health; }
        set { _health = value; }
    }

    public float Armor {
        get { return _armor; }
        set { _armor = value; }
    }

    public float FireRate {
        get { return _fireRate; }
        set { _fireRate = value; }
    }

    public float ProjectileDamage {
        get { return _projectileDamage; }
        set { _projectileDamage = value; }
    }

    public float ProjectileSize {
        get { return _projectileSize; }
        set { _projectileSize = value; }
    }

    public float ProjectileVelocity {
        get { return _projectileVelocity; }
        set { _projectileVelocity = value; }
    }

    public float ProjectileRange {
        get { return _projectileRange; }
        set { _projectileRange = value; }
    }

    public float MovementSpeed {
        get { return _movementSpeed; }
        set { _movementSpeed = value; }
    }

    public float MovementRotationSpeed {
        get { return _movementRotationSpeed; }
        set { _movementRotationSpeed = value; }
    }

    public float TurretRotationSpeed {
        get { return _turretRotationSpeed; }
        set { _turretRotationSpeed = value; }
    }
}