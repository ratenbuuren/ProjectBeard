using UnityEngine;
using System.Collections;

public class HealthUI : MonoBehaviour {

    public GameObject healthbar;

    private float maxHealthHeight = 0;
    private RectTransform rt;

	void Start () {        
        if (healthbar.GetComponent<RectTransform>()) {
            rt = healthbar.GetComponent<RectTransform>();
            maxHealthHeight = rt.rect.height;               
        }
        else
        {
            Debug.LogWarning("HealthUI script missing RectTransform component");
        }	    
    }
    
    void UpdateHealthUI(float scale) {
        if (healthbar.GetComponent<RectTransform>())
        {
            rt.offsetMax = Vector2.down * (1 - (GetComponent<Health>().current() / GetComponent<Health>().maxHP)) * maxHealthHeight;
        }
    }
}
