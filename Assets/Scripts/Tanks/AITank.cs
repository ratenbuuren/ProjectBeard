using System.Collections.Generic;
using UnityEngine;

public class AITank : BaseShooting {
    public float fireRangeThreshold = 4f;
    private GameObject player;

    protected override void Start() {
        base.Start();
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (allPlayers.Length > 0) {
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        } else {
            Debug.Log("Failed to find any player");
        }
        barrelTransform = transform.Find("Barrel");
    }

    void Update() {
        if (player == null) {
            return;
        }
        Vector3 playerPosition = player.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) > fireRangeThreshold) {
            float step = stats.GetStat(StatType.MovementSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        } else {
            Fire();
        }
    }
}