using UnityEngine;
using System.Collections;

public class BossIndicator : MonoBehaviour {

    public float width = 40;
    public float height = 40;
    public GUIStyle bossIndicatorStyle;

    private GameObject boss;

	// Use this for initialization
	void Start () {
        boss = GameObject.Find("BossShip"); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnGUI()
    {
        if (!boss.GetComponent<Renderer>().isVisible)   
        {
            // Show an indicator of where the boss is.  
            Vector3 targetDir = transform.position - boss.transform.position;
            targetDir = new Vector3(-targetDir.x, targetDir.y, targetDir.z);

            Vector2 targetDirScreen = Camera.main.WorldToScreenPoint(transform.position) - Camera.main.WorldToScreenPoint(boss.transform.position);

            float ratioScreen = Screen.height / Screen.width;
            float ratioTarget = targetDirScreen.y / targetDirScreen.x;
            if (ratioScreen > ratioTarget)
            {
                //vertical
                targetDir *= ((Screen.width / 2) / targetDirScreen.x);
            }
            else
            {
                // horizontal
                targetDir *= ((Screen.height / 2) / targetDirScreen.y);
            }
            Vector2 labelPos = Camera.main.WorldToScreenPoint(transform.position + targetDir);
            GUI.Box(new Rect(labelPos.x - (width / 2), labelPos.y - (height / 2), width, height), "", bossIndicatorStyle);         
        }
      
    }
}
