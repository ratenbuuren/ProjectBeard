using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class DoDamageOnHit: MonoBehaviour
{
    public float dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.root.gameObject.GetComponent<Health>())
        {
            collision.gameObject.transform.root.gameObject.GetComponent<Health>().takeDamage(dmg);
        }
    }
}