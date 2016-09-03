using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class DoDamageOnHit: MonoBehaviour
{
    public float dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Health healthComponent = findHealthComponent(collision.gameObject);
        if (healthComponent)
        {
            healthComponent.takeDamage(dmg);            
        }
    }

    /**
     * Returns the first Health component of obj, starting from itself, than its parent, all the way to the root
     */
    private Health findHealthComponent(GameObject obj)
    {
        Health health = obj.GetComponent<Health>();
        if (health)
        {
            return health;
        }

        Transform parent = obj.transform.parent;
        while (parent != null)
        {
            health = parent.GetComponent<Health>();
            if (health != null) {
                return health;
            }
            parent = parent.parent;
        }
        return null;
    }
}