using UnityEngine;
using System.Collections;

public class CreateTrail : MonoBehaviour {
    public enum intervalType {distance, time };
    public GameObject trailPrefab;
    public GameObject trailParent;
    public intervalType type;
    public float interval = 0.15f;
    public string trailName;

    private bool trailEnabled;

    void Start () {
        trailEnabled = true;
        if (!trailPrefab)
        {
            Debug.LogError("CreateTrail script misses trail prefab");
            Application.Quit();
        }
        generate();
    }

    private void generate()
    {
        GameObject tr = Instantiate(trailPrefab) as GameObject;
        tr.transform.position = transform.position;
        if (trailParent)
        {
            tr.transform.parent = trailParent.transform;
        }
        if (!string.IsNullOrEmpty(trailName))
        {
            tr.name = trailName;
        }
        else
        {
            tr.name = "Trail object";
        }
        Invoke("generate", interval);
    }
}