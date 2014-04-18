using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour {

	Animator cannon_animator;
	GameObject player;
	bool is_firing;
	public int burst_size=4;
	public int burst_current=0;
	public float bullet_variance=3f;

	public float time_in_burst=0.5f;
	public float time_between_bursts=2f;
	private HealthbarScript healthBar;
	private int difficulty = 1;
		
	// Use this for initialization
	void Start () {		
		cannon_animator = transform.Find("BossCannon").GetComponent<Animator>();				
		player = GameObject.Find("Player_ship");
		is_firing = false;			
		healthBar = GameObject.Find("BossShip").GetComponent<HealthbarScript>();						
	}
	
	// Update is called once per frame
	void Update () {		
		// Fire large cannon every several seconds
		if(!is_firing){
			StartCoroutine("FireLargeCannon");
		}
		
		// Increase difficulty when health drops.
		if(healthBar.curHealth/ healthBar.maxHealth < 0.5 && difficulty == 1){
			increaseDifficulty();
			difficulty ++;
			renderer.material.color = new Color(0.7f, 0.7f, 0.7f);
		} else if(healthBar.curHealth/ healthBar.maxHealth < 0.2 && difficulty == 2){
			increaseDifficulty();
			difficulty ++;
			renderer.material.color = new Color(0.5f, 0.5f, 0.5f);
		}
		
		float curAngle = transform.rotation.eulerAngles.z;
		
		Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y-10));		
	}
	
	void increaseDifficulty(){
		burst_size += 3;
		time_in_burst *= 0.8f;
		time_between_bursts *= 0.8f;			
	}
	
	IEnumerator FireLargeCannon(){
		is_firing = true;		
		cannon_animator.Play("boss_cannon");
		GameObject cross = Instantiate(Resources.Load("boss/Cross")) as GameObject;
		cross.transform.position = new Vector3(player.transform.position.x+Random.Range(-bullet_variance, bullet_variance), player.transform.position.y+Random.Range(-bullet_variance, bullet_variance), player.transform.position.z);
		
		if(burst_current <= burst_size){
			yield return new WaitForSeconds(time_in_burst);
			burst_current++;
		}else{
			yield return new WaitForSeconds(time_between_bursts);		
			burst_current = 0;
		}
		is_firing = false;
	}
}
