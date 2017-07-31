using System.Collections;
using UnityEngine;

public class BaseShooting : BaseTank {

    public GameObject projectilePrefab;
    
    protected Transform barrelTransform;
    
    private Transform bulletOrigin;
    private bool canShoot = true;
    
    protected override void Start() {
        base.Start();
        barrelTransform = transform.Find("Barrel");
        bulletOrigin = barrelTransform.Find("BulletOrigin");
    }

    protected void Fire() {
        if (canShoot) {
            GameObject bullet = Instantiate(projectilePrefab, bulletOrigin.position, Quaternion.identity);
            bullet.transform.rotation = barrelTransform.rotation;
            bullet.transform.localScale = Vector2.one * stats.GetStat(StatType.ProjectileSize);

            ProjectileController pc = bullet.GetComponent<ProjectileController>();
            pc.Damage = stats.GetStat(StatType.ProjectileDamage);
            pc.Range = stats.GetStat(StatType.ProjectileRange);
            pc.Velocity = stats.GetStat(StatType.ProjectileVelocity);
            pc.Origin = gameObject;
            
            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload() {
        canShoot = false;
        yield return new WaitForSeconds(1/stats.GetStat(StatType.FireRate));
        canShoot = true;
    }
}
