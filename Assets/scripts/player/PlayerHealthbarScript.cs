using UnityEngine;
using System.Collections;

public class PlayerHealthbarScript : MonoBehaviour {
	
	public GUIStyle playerHealthBarStyleOut;
	public GUIStyle playerHealthBarStyleIn;
	public GUIStyle gameOverStyle;
	public float maxHealth=100;
	public float curHealth;	

	
	// Use this for initialization
	void Start () {
		curHealth = maxHealth;		
	}
	
	// Update is called once per frame
	void Update () {	        
	}
	
	void OnTriggerEnter2D(Collider2D collision){
		DoDamageOnHit dmgComponent = collision.gameObject.GetComponent<DoDamageOnHit>();
		if(dmgComponent != null && collision.gameObject.tag != "Friendly"){
			curHealth = curHealth - dmgComponent.dmg;
		}
    }
	
	void OnGUI () {	
		GUI.Box( new Rect(10,10, 50,  300), "", playerHealthBarStyleOut);
		GUI.Box( new Rect(13,13, 44,  294*(curHealth/maxHealth)), Mathf.Round((curHealth/maxHealth)*100)+"%", playerHealthBarStyleIn);
		
		if(curHealth <= 0){
			curHealth = 0;
			GUI.Box( new Rect(0,0, Screen.width,  Screen.height), "You have died..", gameOverStyle);            
		}
	}	
}
