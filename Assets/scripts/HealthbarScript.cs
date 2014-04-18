using UnityEngine;
using System.Collections;

public class HealthbarScript: MonoBehaviour {

	
	public float maxHealth=10;
	public float curHealth;	
	
	// Variables for health bar GUI
	public float guiOffsetY = -80;
	public Vector2 guiSize = new Vector2(30,12);
	
	// Use this for initialization
	void Start () {
		curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {		
		if(curHealth <= 0){
			Destroy(this.gameObject);
			GameObject explosion = Instantiate(Resources.Load("Explosion")) as GameObject;
			explosion.transform.position = transform.position;
		}				
	}
	  
    void OnTriggerEnter2D(Collider2D collision){
		Damage dmgComponent = collision.gameObject.GetComponent<Damage>();
		if(dmgComponent != null){
			curHealth = curHealth - dmgComponent.dmg;
			Destroy(collision.gameObject);
		}
    }

	void OnGUI () {
		Vector2 targetPos = Camera.main.WorldToScreenPoint (transform.position);			
		GUI.Box(	new Rect(targetPos.x - guiSize.x, Screen.height-targetPos.y + guiOffsetY, guiSize.x*2 ,guiSize.y), "");
		GUI.Box(	new Rect(targetPos.x - ((curHealth/maxHealth)*(guiSize.x-1)), Screen.height-targetPos.y + 1 + guiOffsetY, (curHealth/maxHealth)*(guiSize.x-1)*2 , guiSize.y-2), "");
	}	
}
