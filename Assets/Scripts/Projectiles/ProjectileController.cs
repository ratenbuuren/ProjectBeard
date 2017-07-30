using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    private AmmoType ammoType;
    private float damage = 25f;
    private float velocity = 3f;
    private float range = 4f;

    private GameObject origin;

    void Start() {
        Destroy(this.gameObject, range);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != origin) {
            if (other.gameObject.name.Contains("Tank")) {
                other.gameObject.GetComponent<TankHealth>().TakeDamage(damage, ammoType);
            }
            Destroy(gameObject);
        }
    }

    void Update() {
        transform.position += transform.up * velocity * Time.deltaTime;
    }
    
    public AmmoType AmmoType {
        get { return ammoType; }
        set { ammoType = value; }
    }

    public float Damage {
        get { return damage; }
        set { damage = value; }
    }

    public float Velocity {
        get { return velocity; }
        set { velocity = value; }
    }

    public float Range {
        get { return range; }
        set { range = value; }
    }

    public GameObject Origin {
        get { return origin; }
        set { origin = value; }
    }
}