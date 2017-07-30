using System;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : MonoBehaviour {
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _fireRate = 2f; // shots per second
    [SerializeField] private float _projectileDamage = 40f;
    [SerializeField] private float _projectileSize = 1f;
    [SerializeField] private float _projectileVelocity = 1f;
    [SerializeField] private float _projectileRange = 5f;
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _movementRotationSpeed = 2f;
    [SerializeField] private float _turretRotationSpeed = 2f;

    private Dictionary<StatType, float> currentValues = new Dictionary<StatType, float>();
    private Dictionary<StatType, List<PowerUpEntry>> _activePowerUps = new Dictionary<StatType, List<PowerUpEntry>>();

    private void Awake() {
        ResetCurrentValues();
        foreach (StatType statType in Enum.GetValues(typeof(StatType))) {
            _activePowerUps[statType] = new List<PowerUpEntry>();
        }
    }

    private void ResetCurrentValues() {
        currentValues[StatType.MaxHealth] = _maxHealth;
        currentValues[StatType.FireRate] = _fireRate;
        currentValues[StatType.ProjectileDamage] = _projectileDamage;
        currentValues[StatType.ProjectileSize] = _projectileSize;
        currentValues[StatType.ProjectileVelocity] = _projectileVelocity;
        currentValues[StatType.ProjectileRange] = _projectileRange;
        currentValues[StatType.MovementSpeed] = _movementSpeed;
        currentValues[StatType.MovementRotationSpeed] = _movementRotationSpeed;
        currentValues[StatType.TurretRotationSpeed] = _turretRotationSpeed;
    }

    private void RecalculateStats() {
        ResetCurrentValues();
        foreach (KeyValuePair<StatType, List<PowerUpEntry>> activePowerUp in _activePowerUps) {
            foreach (PowerUpEntry entry in activePowerUp.Value) {
                if (entry.ApplyType == ApplyType.Addition) {
                    currentValues[activePowerUp.Key] += entry.Amount;
                }
            }
            foreach (PowerUpEntry entry in activePowerUp.Value) {
                if (entry.ApplyType == ApplyType.Multiply) {
                    currentValues[activePowerUp.Key] *= entry.Amount;
                }
            }
        }
    }

    public float GetStat(StatType statType) {
        return currentValues[statType];
    }

    public void AddStats(List<PowerUpEntry> entries) {
        foreach (PowerUpEntry entry in entries) {
            _activePowerUps[entry.StatType].Add(entry);
        }
        RecalculateStats();
    }
}