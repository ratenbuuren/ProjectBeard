using UnityEngine;
using System.Collections;

public class CrossScript : MonoBehaviour {

	public int it=8;

	// Use this for initialization
	void Start () {
		StartCoroutine("turnDark");
		StartCoroutine("turnDarkest");
	}
	
	// Update is called once per frame
	void Update () {	
	}
	
	IEnumerator turnDark(){
		yield return new WaitForSeconds(0.5f);		
		renderer.material.color = new Color(0.6f, 0.6f, 0.6f);
	}
	
	IEnumerator turnDarkest(){
		yield return new WaitForSeconds(0.75f);		
		renderer.material.color = new Color(0.3f, 0.3f, 0.3f);
				
		// Create the explosion.
		GameObject explosion = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
		explosion.transform.position = transform.position;
		Explosion_script script = explosion.GetComponent<Explosion_script>();
		script.type = "c";
		script.scale = 1f;
		script.it = it;
		script.Init();
		
		yield return new WaitForSeconds(0.25f);
		
		Destroy(this.gameObject);
	}	
}
