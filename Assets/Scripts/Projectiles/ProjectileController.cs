using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour {
    public float damageModifier = 1f;

    protected AmmoType ammoType = AmmoType.Normal;
    protected float baseDamage = 25f;
    protected float velocity = 3f;
    protected float range = 4f;
    protected float scale = 1f;
    protected GameObject origin;

    void Start() {
        Destroy(this.gameObject, range);
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision with" +other.gameObject.name);
        if (other.gameObject != origin) {
            onHit(other);
            Destroy(gameObject);
        }
    }

    protected abstract void onHit(Collider2D other);

    void Update() {
        transform.position += transform.up * velocity * Time.deltaTime;
    }

    public AmmoType AmmoType {
        get { return ammoType; }
        set { ammoType = value; }
    }

    public float Damage {
        get { return baseDamage * damageModifier; }
    }

    public float BaseDamage {
        get { return baseDamage; }
        set { baseDamage = value; }
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

    public float Scale {
        get {
            return scale; 
        }
        set {
            scale = value;
            transform.localScale = Vector3.one*value;
        }
    }
}