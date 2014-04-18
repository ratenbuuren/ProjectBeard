using UnityEngine;
using System.Collections;

public class Captain_FlyingDutchman : Captain{

	//public string name;
	//public int id;
	
	public Captain_FlyingDutchman() : base("Flying Dutchman" , 1){		
	}
	
	public override void Activate(){
		Physics2D.IgnoreLayerCollision (8,9,true);
	}

	public override void Deactivate(){
		Physics2D.IgnoreLayerCollision (8,9,false);
	}
}
