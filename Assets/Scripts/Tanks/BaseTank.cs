using UnityEngine;

[RequireComponent(typeof(TankStats))]
public class BaseTank : MonoBehaviour {

	protected TankStats stats;

	protected virtual void Start() {
		stats = GetComponent<TankStats>();
	}
}
