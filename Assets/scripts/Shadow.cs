using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	public float y_offset=-0.1f;
	public float x_offset;

	// Use this for initialization
	void Start () {
		x_offset = transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector2(transform.parent.position.x + x_offset, transform.parent.position.y + y_offset);
	}
}
