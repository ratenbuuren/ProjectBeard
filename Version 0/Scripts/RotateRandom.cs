using UnityEngine;
using System.Collections;

public class RotateRandom : MonoBehaviour {

    private float rotation;

	// Use this for initialization
	void Start () {
        rotation = Random.Range(-90, 90);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
	}
}
