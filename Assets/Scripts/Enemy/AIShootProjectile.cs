using UnityEngine;
using System.Collections;

public class AIShootProjectile : MonoBehaviour {

    public float velocity = 700f;
    public GameObject target;           /* target to shoot at */
    public GameObject projectilePrefab; /* projectile to generate */
    public GameObject projectileParent;
    public Vector2 shootDirection;      /* relative direction to shoot at */
    public float shootAngle = 30f;          /* angle between object and target should be less than shootAngle before object starts shooting */
    public float fireDelay = 0.2f;      /* delay between shots */
    public float range;                 /* range between target and object at which the object starts shooting */    

    private bool fireEnabled = true;

	void Update () {
        if (target && projectilePrefab && Vector3.Distance(transform.root.position, target.transform.position) <= range && fireEnabled && acceptAngle())
        {
            fireEnabled = false;            
            Invoke("enableFire", fireDelay);
                        
            GameObject p = Instantiate<GameObject>(projectilePrefab);
            p.transform.position = transform.position;            
            p.transform.rotation = transform.rotation;
            p.GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(shootDirection == Vector2.zero ? Vector2.up : shootDirection).normalized * velocity);
            p.layer = LayerMask.NameToLayer("ProjectileEnemy");
            if (p.GetComponent<DoDamageOnHit>())
            {
                p.GetComponent<DoDamageOnHit>().dmg = 10;
            }
            if (projectileParent)
            {
                p.transform.parent = projectileParent.transform;
            }
            if (p.GetComponent<TrailRenderer>())
            {
                p.GetComponent<TrailRenderer>().material.color = Color.black;
            }
        }
	}

    private void OnDrawGizmosSelected()
    {
        /* for debuggin purposes, draw the triggerAtDistance around the object*/
        UnityEditor.Handles.color = acceptAngle() ? Color.red : Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.root.position, Vector3.forward, range);
        UnityEditor.Handles.DrawLine(transform.root.position, transform.root.position + transform.TransformDirection(Quaternion.Euler(0, 0, shootAngle) * shootDirection * range));
        UnityEditor.Handles.DrawLine(transform.root.position, transform.root.position + transform.TransformDirection(Quaternion.Euler(0, 0, -shootAngle) * shootDirection * range));

    }


    void enableFire()
    {
        fireEnabled = true;
    }

    private bool acceptAngle()
    {
        return Vector2.Angle(transform.root.position, target.transform.position) < shootAngle;
    }
}
