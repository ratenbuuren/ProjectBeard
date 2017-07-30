using UnityEngine;

[RequireComponent(typeof(TankStats))]
public class BaseTank : MonoBehaviour {

	protected TankStats stats;

	public virtual void Start() {
		stats = GetComponent<TankStats>();
	}
	
	public float GetHealth(){
		return stats.Health;
	}

	public void TakeDamage(float amount) {
		stats.Health -= amount;
		if (stats.Health <= 0) {
			GameManager.instance.RemovePlayer(this.gameObject);
			Destroy(gameObject);
		}
	}
}
