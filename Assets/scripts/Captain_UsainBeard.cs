using UnityEngine;
using System.Collections;

public class Captain_UsainBeard : Captain{

	public string name;
	public int id;
	
	public Captain_UsainBeard() : base("Usain Beard", 0){				
	}
	
	public override void Activate(){
		if (player.powerEnabled) {
				player.powerEnabled = false;				
				player.resetSpeed (player.speed, player.maxSpeed, player.acceleration);

				player.speed *= 3;
				player.maxSpeed *= 3;
				player.acceleration *= 3;
		}
	}

	public override void Deactivate(){
		}


}
