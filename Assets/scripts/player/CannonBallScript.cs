using UnityEngine;
using System.Collections;

public class CannonBallScript : MonoBehaviour {

	float lifeTime;
	float TTL;	

	// Use this for initialization
	void Start () {
		lifeTime = 0;
		TTL = 3;
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime += Time.deltaTime;
		if(lifeTime >= TTL){
			Destroy(this.gameObject);
		}		
	}
}
