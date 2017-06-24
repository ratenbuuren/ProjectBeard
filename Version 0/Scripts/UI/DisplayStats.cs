using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour {

    public GameObject textObject;
    private bool enable = true;

    // Use this for initialization
    void Start() {        
        if (textObject.GetComponent<Text>() == null)
        {
            enable = false;
            Debug.LogError("GUI text is missing, can't display stats");
        }
	}
	
	public void UpdateStats(string stats)
    {
        if (enable)
        {
            textObject.GetComponent<Text>().text = stats;
        }        
    }
}
