using UnityEngine;
using System;

public class AIShootProjectile : MonoBehaviour {

    public GameObject target;           /* target to shoot at */
    public GameObject projectilePrefab; /* projectile to generate */
    public GameObject projectileParent;
    public Vector2 shootDirection;      /* relative direction to shoot at */
    public float velocity   = 700f;
    public float shootAngle = 30f;      /* angle between object and target should be less than shootAngle before object starts shooting */
    public float fireDelay  = 0.2f;     /* delay between shots */
    public float range      = 30f;      /* range between target and object at which the object starts shooting */    

    private bool fireEnabled = true;

	void Update () {
        if (target && projectilePrefab && fireEnabled && withinReach())
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

    private void OnDrawGizmos()
    {
        /* for debugging purposes, draw the triggerAtDistance around the object*/
        UnityEditor.Handles.color = withinReach() ? Color.red : withinRange() ? Color.blue : Color.green;
        //UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, range);
        UnityEditor.Handles.DrawWireArc(transform.position, Vector3.forward, Vector2.up.Rotate(transform.rotation.eulerAngles.z - shootAngle) * range, shootAngle * 2, range);
        UnityEditor.Handles.DrawLine(transform.position, transform.position + transform.TransformDirection(Quaternion.Euler(0, 0, shootAngle) * shootDirection * range));
        UnityEditor.Handles.DrawLine(transform.position, transform.position + transform.TransformDirection(Quaternion.Euler(0, 0, -shootAngle) * shootDirection * range));

    }

    void enableFire()
    {
        fireEnabled = true;
    }
    
    private bool withinReach()
    {
        return withinRange() && withinAngle();
    }

    private bool withinRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= range;
    }

    private bool withinAngle()
    {        
        return Util.angle(Vector2.up.Rotate(transform.rotation.eulerAngles.z) * range, target.transform.position - transform.position) < shootAngle;
    }
}
