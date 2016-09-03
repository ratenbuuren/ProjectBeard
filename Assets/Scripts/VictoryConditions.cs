using UnityEngine;
using System.Collections;

public class VictoryConditions : MonoBehaviour {

	public HealthbarScript bossHealthBar;
	public GUIStyle gameOverStyle;

	// Use this for initialization
	void Start () {
		bossHealthBar = GameObject.Find("BossShip").GetComponent<HealthbarScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {			
		if(bossHealthBar == null || bossHealthBar.curHealth <= 0){
			GUI.Box( new Rect(0,0, Screen.width,  Screen.height), "You have defeated the boss, congratulations!", gameOverStyle);
		}		
	}	
}
