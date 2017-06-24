using UnityEngine;
using System.Collections;

public class SuicideScript : MonoBehaviour {
	
	int speed = 3;
	int rotationSpeed = 7;
	int distance = 10;
	GameObject player;
	Transform target;
	Transform enemyTransform;
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemyTransform = this.GetComponent<Transform>();
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		int dist = (int) Vector2.Distance(player.transform.position, this.transform.position);
		if (dist < distance) {
						//	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - this.transform.position),rotationSpeed * Time.deltaTime);
						//	transform.position += transform.forward * speed * Time.deltaTime;
						//}

						target = GameObject.FindWithTag ("Player").transform;
						//Vector2 targetHeading = target.position - transform.position;
						//Vector2 targetDirection = targetHeading.normalized;

		
						//rotate to look at the player
		
						//transform.rotation = Quaternion.LookRotation(targetDirection); // Converts target direction vector to Quaternion
						//transform.eulerAngles = new Vector2(0, transform.eulerAngles.z);

						transform.LookAt (target.position);
						transform.Rotate (new Vector2 (180, 90), Space.Self);//correcting the original rotation
		
						//move towards the player
						enemyTransform.position += enemyTransform.forward * speed * Time.deltaTime;
						//transform.Translate(new Vector2(speed* Time.deltaTime,0) );
				}
	}
}
