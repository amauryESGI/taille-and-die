using UnityEngine;

public class HealthTrigger : MonoBehaviour {
    [SerializeField]
    public int Heal = 1;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.isTrigger != true && other.CompareTag("Player")) {
            other.SendMessageUpwards("Heal", Heal);

            Destroy(transform.parent.gameObject);
        }
    }
}