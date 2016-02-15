using Platformer.Character;
using UnityEngine;

public class Fire : MonoBehaviour {
    [SerializeField] private float _knockbackPwd;
    [SerializeField] private float _knockbackTime;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var g = other.gameObject;
            g.GetComponent<Health>().Damage(1);

            bool tmp = other.transform.position.x < transform.position.x;

            StartCoroutine(g.GetComponent<PlatformerCharacter>()
                .Knockback(_knockbackPwd, _knockbackTime, other.transform.position.x < transform.position.x));
        }
    }
}
