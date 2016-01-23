using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class CreateObjectOnHit : MonoBehaviour {

    public GameObject createdObject;
    public GameObject parent;
    public Color color = Color.white;
    public string tagConstraint;

    void OnTriggerEnter2D(Collider2D collision)
    {        
        if (createdObject && (string.IsNullOrEmpty(tagConstraint) || tagConstraint == collision.transform.tag))
        {
            GameObject newObject = Instantiate(createdObject) as GameObject;
            newObject.transform.LookAt(collision.transform.position);
            newObject.transform.position = collision.transform.position;
            if (parent)
            {
                newObject.transform.parent = parent.transform;                
            }            
            
            if (color != Color.white && createdObject.GetComponent<Renderer>().sharedMaterial)
            {
                createdObject.GetComponent<Renderer>().sharedMaterial.color = color;            
            }            
        }        
    }

}
