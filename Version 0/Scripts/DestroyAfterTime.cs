using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float time = 0f;
    public bool hasEndAnimation=false;
    public float endAnimationDuration=0f;

	// Use this for initialization
	void Start () {
        if(hasEndAnimation && endAnimationDuration > 0)
        {
            Invoke("startEndAnimation", time - endAnimationDuration);
        }        
        Invoke("destroy", time);
	}
	
    private void startEndAnimation()
    {
        GetComponent<Animator>().SetTrigger("End");
    }

    public void destroy ()
    {
        Destroy(this.gameObject);
    }
}
