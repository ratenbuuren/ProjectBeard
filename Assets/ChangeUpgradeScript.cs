using UnityEngine;
using System.Collections;

public class ChangeUpgradeScript : MonoBehaviour {

	private ShipMovementScript player_script;

	// Use this for initialization
	void Start () {		
		player_script = GameObject.FindWithTag("Player").GetComponent("ShipMovementScript") as ShipMovementScript;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnCollisionEnter2D(Collision2D collision){				
		if(collision.gameObject.tag == "Player"){
			player_script.changeUpgrade();
			Destroy(this.gameObject);
		}
    }
}
