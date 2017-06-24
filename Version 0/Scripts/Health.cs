using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float maxHP;    
    public bool dieOnZero = true;
    public GameObject[] explosionPrefabs;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;        
    }

	public void takeDamage(float dmg)
    {
        currentHP -= dmg;        
        if (currentHP <= 0 && dieOnZero)
        {
            Destroy(gameObject);
            for (int i = 0; i < explosionPrefabs.Length; i++){
                GameObject explosion = Instantiate(explosionPrefabs[i]) as GameObject;
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
            }
        }
        gameObject.SendMessage("UpdateHealthUI", currentHP/maxHP, SendMessageOptions.DontRequireReceiver);
    }

    public float current()
    {
        return currentHP;
    }
}