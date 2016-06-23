using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Character {
    public class Restarter : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Player")
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}