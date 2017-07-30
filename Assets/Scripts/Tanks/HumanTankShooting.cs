using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTankShooting : BaseTank {
    public GameObject projectilePrefab;
    private Transform barrelTransform;
    private Transform bulletOrigin;

    public int rotationOffset = 270;
    public bool controller = false;
    public float rotationSpeed = 30f;

    private string fireInput;
    private string rotateAxis;

    protected override void Start() {
        base.Start();
        barrelTransform = transform.Find("Barrel");
        bulletOrigin = barrelTransform.Find("BulletOrigin");
    }

    public void SetFireInput(string fireInput, string rotateAxis, bool controller) {
        this.fireInput = fireInput;
        this.rotateAxis = rotateAxis;
        this.controller = controller;
    }

    void Update() {
        if (Input.GetButtonDown(fireInput)) {
            GameObject bullet = Instantiate(projectilePrefab, bulletOrigin.position, Quaternion.identity);
            bullet.transform.rotation = barrelTransform.rotation;
            bullet.transform.localScale = Vector2.one * stats.GetStat(StatType.ProjectileSize);

            ProjectileController pc = bullet.GetComponent<ProjectileController>();
            pc.Damage = stats.GetStat(StatType.ProjectileDamage);
            pc.Range = stats.GetStat(StatType.ProjectileRange);
            pc.Velocity = stats.GetStat(StatType.ProjectileVelocity);
            pc.Origin = gameObject;
        }
    }

    void FixedUpdate() {
        if (controller) {
            barrelTransform.Rotate(0, 0, Input.GetAxis(rotateAxis) * rotationSpeed * Time.deltaTime);
        } else {
            // This will calculate the distance between the mouse in the game and the position of the tank turret
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - barrelTransform.position;
            // This returns simplified values which makes it easier to work with
            difference.Normalize();

            // This calculates the angle between the mouse and the turret by using the values derives from the difference calculation.
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            // This will rotate the turret towards the calculated angle over time. Tweaking the multiplication value will state how quickly or slowly it will rotate.
            barrelTransform.rotation = Quaternion.RotateTowards(barrelTransform.rotation,
                Quaternion.Euler(0f, 0f, angle + rotationOffset), 1000 * Time.deltaTime);
        }
    }
}