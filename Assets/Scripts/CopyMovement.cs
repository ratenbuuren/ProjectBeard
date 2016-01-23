using UnityEngine;
using System.Collections;

public class CopyMovement : MonoBehaviour {

    public GameObject target;
    public Vector3 positionOffset = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;

	// Use this for initialization
	void Start () {
	    if (!target)
        {
            Debug.LogError("No target, destroy object");
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + positionOffset;
        transform.rotation = target.transform.rotation;
        transform.Rotate(rotationOffset);
	}
}
