using UnityEngine;
using System.Collections;

public class DestroyOnCollisionWithObject : MonoBehaviour
{
    public GameObject collisionObject;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == collisionObject)
        {
            Destroy(gameObject);
        }        
    }
}