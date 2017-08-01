using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooting : BaseTank {
    public GameObject normalAmmoPrefab;
    public GameObject piercingAmmoPrefab;
    public GameObject explosiveAmmoPrefab;
    
    protected Transform barrelTransform;

    private Transform bulletOrigin;
    private bool canShoot = true;
    private Dictionary<AmmoType, GameObject> _prefabDictionary;

    protected override void Start() {
        base.Start();
        barrelTransform = transform.Find("Barrel");
        bulletOrigin = barrelTransform.Find("BulletOrigin");
        _prefabDictionary = new Dictionary<AmmoType, GameObject>() {
            {AmmoType.Normal, normalAmmoPrefab},
            {AmmoType.ArmorPiercing, piercingAmmoPrefab},
            {AmmoType.Explosive, explosiveAmmoPrefab}
        };
    }

    protected void Fire() {
        if (canShoot) {
            GameObject bullet = Instantiate(_prefabDictionary[stats.AmmoType], bulletOrigin.position, Quaternion.identity);
            bullet.transform.rotation = barrelTransform.rotation;
            bullet.transform.localScale = Vector2.one * stats.GetStat(StatType.ProjectileSize);

            ProjectileController pc = bullet.GetComponent<ProjectileController>();
            pc.Damage = stats.GetStat(StatType.ProjectileDamage);
            pc.Range = stats.GetStat(StatType.ProjectileRange);
            pc.Velocity = stats.GetStat(StatType.ProjectileVelocity);
            pc.AmmoType = stats.AmmoType;
            pc.Origin = gameObject;

            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload() {
        canShoot = false;
        yield return new WaitForSeconds(1 / stats.GetStat(StatType.FireRate));
        canShoot = true;
    }
}