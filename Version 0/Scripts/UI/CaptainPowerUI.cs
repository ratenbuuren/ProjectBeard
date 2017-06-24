using UnityEngine;
using System.Collections;

public class CaptainPowerUI : MonoBehaviour {

    public GameObject bar;

    private float maxHeight = 0;
    private bool onCooldown;
    private float cd;
    private float cdRemaining;
    private RectTransform rt;

    void Start()
    {
        if (bar.GetComponent<RectTransform>())
        {
            rt = bar.GetComponent<RectTransform>();
            maxHeight = rt.rect.height;
        }
        else
        {
            Debug.LogWarning("CaptainPowerUI script missing RectTransform component");
        }
        onCooldown = false;        
    }

    void Update()
    {        
        if (bar.GetComponent<RectTransform>() && onCooldown)
        {
            cdRemaining -= Time.deltaTime;
            if (cdRemaining <= 0)
            {
                onCooldown = false;
            }
            rt.offsetMax = Vector2.down * (cdRemaining / cd) * maxHeight;            

        }        
    }

    void UpdateCaptainPowerUI(float cd)
    {
        this.cd = cd;
        cdRemaining = cd;
        onCooldown = true;
    }
}
