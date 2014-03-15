using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	int zoom=40;
	int normal=60;		
	GameObject player_ship;
	ShipMovementScript script;
	
	float minSpeed;
	float maxSpeed;
	
	// Use this for initialization
	void Start () {
		player_ship = GameObject.Find("Player_ship");
		script = player_ship.GetComponent<ShipMovementScript>();
		Camera.main.orthographic  = true;
		minSpeed = script.minSpeed;
		maxSpeed = script.maxSpeed;		
	}
	
	// Update is called once per frame
		
	void Update () {
		float zoomLevel = Mathf.Lerp(zoom, normal, script.speed/((maxSpeed - minSpeed)))/6;		
		Camera.main.orthographicSize = zoomLevel;		
		transform.position = new Vector3(player_ship.transform.position.x, player_ship.transform.position.y, transform.position.z);				
	}
}
