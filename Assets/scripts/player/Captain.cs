using UnityEngine;
using System.Collections;

public abstract class Captain : MonoBehaviour{
	
	new public string name;
	public int id;
	public ShipMovementScript player = GameObject.FindGameObjectWithTag ("Player").GetComponent("ShipMovementScript") as ShipMovementScript;
	
	public Captain(string name, int id){
		this.name = name;
		this.id = id;
	}
	
	public abstract void Activate(); 	
	public abstract void Deactivate();
	
	public int getId(){
		return id;
	}
}
