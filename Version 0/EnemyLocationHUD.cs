using UnityEngine;
using System.Collections;

public class EnemyLocationHUD : MonoBehaviour {

    public float distanceFromPlayer = 6f;
    public Texture indicator;
    public Vector2 indicatorSize;

	// Use this for initialization
	void Start () {
	    if (!indicator)
        {
            Debug.LogError("EnemyLocationHUD misses indicator texture.");            
        }
        Debug.Log("Currently " + GameObject.FindGameObjectsWithTag("Enemy").Length +" enemies in game");
	}

    void OnGUI() {
        if (indicator)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {                
                if (!isInScreen(enemy))
                {
                    GUI.DrawTexture(indicatorSize == Vector2.zero ? rect(enemy, new Vector2(indicator.width, indicator.height)) : rect(enemy, indicatorSize), indicator, ScaleMode.ScaleToFit);
                }                
            }            
        }        
	}

    private void OnDrawGizmosSelected()
    {
         UnityEditor.Handles.color = Color.white;
         UnityEditor.Handles.DrawWireDisc(transform.root.position, Vector3.forward, distanceFromPlayer);        
    }

    private bool isInScreen(GameObject enemy)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
        return screenPos.x >= 0 && screenPos.x <= 1 && screenPos.y >= 0 && screenPos.y <= 1;
    }

    private Rect rect(GameObject obj, Vector2 size)
    {
        //Vector2 size = new Vector2(indicator.width, indicator.height);
        Vector2 pos = (new Vector3(obj.transform.position.x - transform.position.x, transform.position.y - obj.transform.position.y).normalized * distanceFromPlayer) + transform.position;        
        pos = Camera.main.WorldToScreenPoint(pos); /* map world coordinates to screen coordinates*/
        pos = new Vector2(pos.x - (0.5f * size.x), pos.y - (0.5f * size.y)); /* offset for the height and width of the image */                      
        return new Rect(pos, size);
    }
}
