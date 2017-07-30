using UnityEngine;

public class AITank : BaseShooting {
    public float fireRangeThreshold = 4f;
    private GameObject player;

    protected override void Start() {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        barrelTransform = transform.Find("Barrel");
    }

    void Update() {
        Vector3 playerPosition = player.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) > fireRangeThreshold) {
            float step = stats.GetStat(StatType.MovementSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
        } else {
            Fire();
        }
    }
}