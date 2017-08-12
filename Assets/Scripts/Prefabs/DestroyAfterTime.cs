using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float lifetime = 1f;

    // Use this for initialization
    void Start () {
        Invoke("Destroy", lifetime);
    }
	
    private void Destroy ()
    {
        Destroy(this.gameObject);
    }
}
