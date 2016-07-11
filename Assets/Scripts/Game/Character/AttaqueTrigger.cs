using UnityEngine;
using System.Collections;

public class AttaqueTrigger : MonoBehaviour {
    [SerializeField] public int damage = 1;
    void OnTriggerEnter2D (Collider2D other) {
        //if (other.isTrigger != true && other.CompareTag("Ennemy"))
            //other.SendMessageUpwards("Damage", damage);
	}
}
