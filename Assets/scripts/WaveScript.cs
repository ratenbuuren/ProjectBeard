using UnityEngine;
using System.Collections;

public class WaveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {}
	
	void Die(){
		Destroy(this.gameObject);
	}
}
