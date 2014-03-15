using UnityEngine;
using System.Collections;

public class ShipEnemyCollisionScript : MonoBehaviour {

	private float curHealth;
	private float maxHealth;
	private float healthBarLength;
	
	// Variables for health bar GUI
	private float guiOffsetY;
	private Vector2 guiSize;
	private Texture2D healthTexture;
	

	// Use this for initialization
	void Start () {
		maxHealth = 10;
		curHealth = maxHealth;
		healthBarLength = 0;
		
		guiOffsetY = -80;
		guiSize = new Vector2(30,12);
		healthTexture = Resources.Load("healthbar") as Texture2D;
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
		Destroy(collision.gameObject);
		curHealth --;
    }

	void OnGUI () {
		Vector2 targetPos = Camera.main.WorldToScreenPoint (transform.position);	
		//GUI.Label(	new Rect(targetPos.x - guiSize.x, Screen.height-targetPos.y + guiOffsetY, guiSize.x*2 ,20), "");
		GUI.Box(	new Rect(targetPos.x - guiSize.x, Screen.height-targetPos.y + guiOffsetY, guiSize.x*2 ,12), "");
		GUI.Box(	new Rect(targetPos.x - ((curHealth/maxHealth)*(guiSize.x-1)), Screen.height-targetPos.y + 1 + guiOffsetY, (curHealth/maxHealth)*(guiSize.x-1)*2 , 10), "");
	}	
}
