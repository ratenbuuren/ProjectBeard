using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

    public float velocity = 700f;
    public GameObject projectilePrefab;
    public GameObject projectileParent;
    public float fireDelay = 0.2f;
    public Vector2 shootDirection;

    private bool fireEnabled = true;

    void Start()
    {
        if (!projectilePrefab)
        {
            Debug.LogError("ShootProjectile script misses projectile prefab");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update () {
        // Shoot cannonball
        if (Input.GetKey(KeyCode.Space))
        {
            if (fireEnabled)
            {
                fireEnabled = false;
                Invoke("enableFire", fireDelay);

                GameObject p = Instantiate<GameObject>(projectilePrefab);
                p.transform.position = transform.position;
                p.transform.rotation = transform.rotation;
                p.GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(shootDirection == Vector2.zero ? Vector2.up : shootDirection).normalized * velocity);
                p.layer = LayerMask.NameToLayer("ProjectilePlayer");
                if (projectileParent)
                {
                    p.transform.parent = projectileParent.transform;
                }
                if (p.GetComponent<TrailRenderer>())
                {
                    p.GetComponent<TrailRenderer>().material.color = Color.white;
                }
            }
        }
    }

    private void enableFire()
    {
        fireEnabled = true;
    }
}
