using UnityEngine;
using System.Collections;

public class Explosion_script : MonoBehaviour {

	public string type;
	public int it;
	public float scale;
	private float d_to_child;

	// Use this for initialization
	void Start () {						
	}
	
	public void Init(){
		transform.localScale = new Vector3(scale, scale, scale);
		d_to_child = GetComponent<Renderer>().bounds.size.x/2;
		StartCoroutine("destroy");
		StartCoroutine("generateChildren");				
	}
	
	IEnumerator generateChildren(){			
		yield return new WaitForSeconds(0.05f);	
		// Only generate children if there are it left		
		if(it >= 1){	
			switch(type){
				// create four children
				case "c":				
					GameObject lt = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					lt.transform.position = new Vector3(transform.position.x-d_to_child, transform.position.y+d_to_child);					
					Explosion_script lt_script = lt.GetComponent<Explosion_script>();					
					initChild(lt_script, "lt", 0.5f*scale, it-1);

					GameObject rt = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					rt.transform.position = new Vector3(transform.position.x+d_to_child, transform.position.y+d_to_child);					
					Explosion_script rt_script = rt.GetComponent<Explosion_script>();					
					initChild(rt_script, "rt", 0.5f*scale, it-1);		

					GameObject lb = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					lb.transform.position = new Vector3(transform.position.x-d_to_child, transform.position.y-d_to_child);					
					Explosion_script lb_script = lb.GetComponent<Explosion_script>();					
					initChild(lb_script, "lb", 0.5f*scale, it-1);

					GameObject rb = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					rb.transform.position = new Vector3(transform.position.x+d_to_child, transform.position.y-d_to_child);					
					Explosion_script rb_script = rb.GetComponent<Explosion_script>();					
					initChild(rb_script, "rb", 0.5f*scale, it-1);						
				break;				
				
				// left top child
				case "lt":
					lt = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					lt.transform.position = new Vector3(transform.position.x-d_to_child, transform.position.y+d_to_child);					
					lt_script = lt.GetComponent<Explosion_script>();					
					initChild(lt_script, "lt", 0.9f*scale, it-1);
				break;
				
				// right top child
				case "rt":
					rt = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					rt.transform.position = new Vector3(transform.position.x+d_to_child, transform.position.y+d_to_child);					
					rt_script = rt.GetComponent<Explosion_script>();					
					initChild(rt_script, "rt", 0.9f*scale, it-1);	
				break;
				
				// left bottom child
				case "lb":
					lb = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					lb.transform.position = new Vector3(transform.position.x-d_to_child, transform.position.y-d_to_child);					
					lb_script = lb.GetComponent<Explosion_script>();					
					initChild(lb_script, "lb", 0.9f*scale, it-1);
				break;
				
				// right bottom child
				case "rb":
					rb = Instantiate(Resources.Load("boss/Boss_explosion")) as GameObject;
					rb.transform.position = new Vector3(transform.position.x+d_to_child, transform.position.y-d_to_child);					
					rb_script = rb.GetComponent<Explosion_script>();					
					initChild(rb_script, "rb", 0.9f*scale, it-1);
				break;
				default:
				break;			
			}
		}

	}
	
	private void initChild(Explosion_script script, string script_type, float script_scale, int script_it){
		script.type = script_type;
		script.scale = script_scale;
		script.it = script_it;
		script.Init();
	}
	
	IEnumerator destroy(){
		yield return new WaitForSeconds(0.4f);
		Destroy(this.gameObject);		
	}
	
	// Update is called once per frame
	void Update () {}
}
