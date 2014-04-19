using UnityEngine;
using System.Collections;

public class ShipMovementScript : MonoBehaviour {

	private float 	torque;
	private float 	maxTorque;
	
	public 	float 	minSpeed;
	public	float 	maxSpeed;
	public 	float 	speed;
	public	float 	acceleration;
	
	private float 	fireRate;
	private float 	fireCounter;
	private bool	fireEnabled;

	public bool	powerEnabled;

	private float 	waveCounter;
	private float	waveRate;

	private float	v_cannon;
	private Captain	captain;	
	
	// Use this for initialization
	void Start () {
		torque = 100;
		maxTorque = 400;
		
		minSpeed = 1;		
		maxSpeed = 3;
		speed = maxSpeed;
		acceleration = 0.01f;		
		
		fireRate = 0.2f;
		fireCounter = 0;
		fireEnabled = true;
		powerEnabled = true;
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, minSpeed);
		
		v_cannon = 700;
		
		waveRate = 0.15f;
		waveCounter = 0;
		try{
			captain = new Captain_UsainBeard();		
		} catch(UnityException){}
	}
	
	// Update is called once per frame
	void Update () {
		float av = rigidbody2D.angularVelocity;
		float a = transform.eulerAngles.z;				
		
		// Change render direction of the ship
		if(Input.GetKey(KeyCode.LeftArrow)){
			
			// If maxTorque is not yet reached, keep addingTorque
			if(av < maxTorque){
				rigidbody2D.AddTorque(torque);
				// If currently turning right, add more torque to the left
				if(av < 0){
					rigidbody2D.AddTorque(torque);
				}
			} 
			
		} else if(Input.GetKey(KeyCode.RightArrow)){
			if(av > -maxTorque){
				rigidbody2D.AddTorque(-torque);
				if(av > 0){
					rigidbody2D.AddTorque(-torque);
				}
			}			
		}

		// Change the speed of the ship
		if(Input.GetKey(KeyCode.UpArrow)){
			if(speed  <= maxSpeed){
				speed = speed + acceleration;
			}			
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			if(speed >= minSpeed){
				speed = speed - acceleration;
			}			
		}
		
		// Change movement direction of the ship
		rigidbody2D.velocity = Vector2.zero;
		Vector2 v_dir = new Vector2(-Mathf.Sin(a*Mathf.Deg2Rad), Mathf.Cos(a*Mathf.Deg2Rad));
		rigidbody2D.AddForce(v_dir.normalized*speed*100);		
		//Debug.DrawLine(transform.position, new Vector2(transform.position.x+v_dir.x, transform.position.y+v_dir.y));		
		
		if(fireEnabled == false){
			fireCounter += Time.deltaTime;			
			if(fireCounter > fireRate){
				fireEnabled = true;
				fireCounter = 0;
			}
		}

		// Shoot cannonball
		if(Input.GetKey(KeyCode.Space)){
			if(fireEnabled){
				fireEnabled = false;
				
				Vector2 c_dir = new Vector2(-Mathf.Sin((a+90)*Mathf.Deg2Rad), Mathf.Cos((a+90)*Mathf.Deg2Rad));			
				GameObject cannonball = Instantiate(Resources.Load("Cannonball")) as GameObject;			
				cannonball.transform.position = new Vector2(transform.position.x-0.8f, transform.position.y);
				cannonball.transform.RotateAround(transform.position, Vector3.up, a);
				cannonball.transform.rotation = Quaternion.identity;
				cannonball.rigidbody2D.AddForce(c_dir.normalized * v_cannon);
			}
		}
		
		// Leave waves behind
		waveCounter += Time.deltaTime;
		if(waveCounter >= waveRate){
			GameObject temp = Instantiate(Resources.Load("Wave")) as GameObject;
			temp.transform.position = transform.position;
			waveCounter = 0;
		}
		
		// Use Captains power
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			captain.Activate();
		}
	}
	
	
	
	void OnGUI () {
		Vector2 targetPos = Camera.main.WorldToScreenPoint (transform.position);	
		GUI.Label(new Rect(targetPos.x-50, Screen.height-targetPos.y, 100 ,20), captain.name);
	}
	
	public void changeUpgrade(){
		captain.Deactivate ();
		if(captain.getId() == 0){
			captain = new Captain_FlyingDutchman();
		} else{
			captain = new Captain_UsainBeard();
		}
		Debug.Log("New captain: " +captain.name);
	}
	
	public void changeUpgrade(int id){}

	private float oldSpeed, oldMaxSpeed, oldAcceleration;

	public void resetSpeed (float speed, float maxSpeed, float acceleration){
		oldSpeed = speed;
		oldMaxSpeed = maxSpeed;
		oldAcceleration = acceleration;
		StartCoroutine("ResetSpeed");
	}

	IEnumerator ResetSpeed(){
		yield return new WaitForSeconds(1);		
		
		this.speed = oldSpeed;
		this.maxSpeed = oldMaxSpeed;
		this.acceleration = oldAcceleration;
		
		yield return new WaitForSeconds(2);
		powerEnabled = true;
	}
}
