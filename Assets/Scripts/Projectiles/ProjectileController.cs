using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour {
    protected AmmoType ammoType = AmmoType.Normal;
    protected float damage = 25f;
    
    private float velocity = 3f;
    private  float range = 4f;

    private GameObject origin;

    void Start() {
        Destroy(this.gameObject, range);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != origin) {
            dealDamage(other);
            Destroy(gameObject);
        }
    }

    protected abstract void dealDamage(Collider2D other);

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