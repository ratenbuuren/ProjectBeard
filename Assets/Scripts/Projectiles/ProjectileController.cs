using UnityEngine;

public abstract class ProjectileController : MonoBehaviour {
    public float DamageModifier = 1f;

    private AmmoType _ammoType = AmmoType.None;
    private float _baseDamage = 25f;
    private float _velocity = 3f;
    private float _range = 4f;
    private float _scale = 1f;
    private GameObject _origin;

    void Start() {
        Destroy(this.gameObject, _range);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != _origin) {
            onHit(other);
            Destroy(gameObject);
        }
    }

    protected abstract void onHit(Collider2D other);

    void Update() {
        transform.position += transform.up * _velocity * Time.deltaTime;
    }

    public AmmoType AmmoType {
        get { return _ammoType; }
        set { _ammoType = value; }
    }

    public float Damage {
        get { return _baseDamage * DamageModifier; }
    }

    public float BaseDamage {
        get { return _baseDamage; }
        set { _baseDamage = value; }
    }

    public float Velocity {
        get { return _velocity; }
        set { _velocity = value; }
    }

    public float Range {
        get { return _range; }
        set { _range = value; }
    }

    public GameObject Origin {
        get { return _origin; }
        set { _origin = value; }
    }

    public float Scale {
        get { return _scale; }
        set {
            _scale = value;
            transform.localScale = Vector3.one * value;
        }
    }
}