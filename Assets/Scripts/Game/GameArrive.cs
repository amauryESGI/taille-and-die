using UnityEngine;
using UnityEngine.SceneManagement;

public class GameArrive : MonoBehaviour {
    [SerializeField] private string _sceneWin;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("WIN !");
        GameStat.Win();
        SceneManager.LoadScene(_sceneWin);
    }
}
