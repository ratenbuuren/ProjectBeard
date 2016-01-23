using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public GameObject target;
    int minZoom=10;
	int maxZoom=15;		    
	GameObject playerShip;
	ShipController controller;	
	
	// Use this for initialization
	void Start () {        
		playerShip = GameObject.FindGameObjectWithTag("Player");
        controller = playerShip.GetComponent<ShipController>();        
		Camera.main.orthographic  = true;				
	}
	
	// Update is called once per frame
		
	void Update () {
		float zoomLevel = Mathf.Lerp(minZoom, maxZoom, (controller.getVelocity()-controller.minVelocity)/(controller.maxVelocity- controller.minVelocity));

        zoomLevel = 20;
		Camera.main.orthographicSize = zoomLevel;
        Vector3 newPosition;
        if (!target)
        {
            newPosition = new Vector3(playerShip.transform.position.x, playerShip.transform.position.y, -10);
        }
        else
        {
            newPosition = new Vector3(target.transform.position.x, target.transform.position.y, -10);
        }
        transform.position = newPosition;
    }        
}
