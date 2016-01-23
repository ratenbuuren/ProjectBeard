using UnityEngine;
using System.Collections;

public abstract class Captain : MonoBehaviour {
	
	private string captainName;
	private int id;
    private float cooldown;
    protected ShipController player;

	public void init(string name, int id, float cooldown){
        player = gameObject.GetComponent<ShipController>();
        this.captainName = name;
		this.id = id;
        this.cooldown = cooldown;
	}
	
	public abstract void Activate(); 	
	public abstract void Deactivate();
	
	public int getId(){
		return id;
	}

    public string getName()
    {
        return captainName;
    }

    public float getCooldown()
    {
        return cooldown;
    }
}
