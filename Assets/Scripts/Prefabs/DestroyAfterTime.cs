using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    public float lifetime = 1f;

    void Start() {
        Invoke("Destroy", lifetime);
    }

    private void Destroy() {
        Destroy(this.gameObject);
    }
}