using UnityEngine;
using System.Collections;

public class ShipCollisionScript : MonoBehaviour {

	private ShipPropertiesScript player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent("ShipPropertiesScript") as ShipPropertiesScript;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision){	
		if (collision.gameObject.tag == "Enemy") {
			Destroy (collision.gameObject);
			player.ChangeHealth(-5);
		}
	}
}
