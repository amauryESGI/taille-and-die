using UnityEngine;

public class AttaqueTrigger : MonoBehaviour {
    [SerializeField] public int damage = 1;
    void OnTriggerEnter2D (Collider2D other) {
        if (other.isTrigger != true && other.CompareTag("Player"))
            other.SendMessageUpwards("Damage", damage);
    }
}
