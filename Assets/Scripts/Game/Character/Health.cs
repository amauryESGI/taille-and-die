using Platformer.Character;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    [SerializeField]
    private int _curHealth;
    [SerializeField]
    private int _maxHealth;

    void Start() {
        _curHealth = _maxHealth;
    }

    void Update() {
        if (_curHealth > _maxHealth)
            _curHealth = _maxHealth;
        else if (_curHealth <= 0)
            Die();
    }

    void Die() {
        Debug.Log("Is dead !");

        if (transform.GetComponent<PlatformerUserControl>())
            // Restart
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            Destroy(transform.gameObject);
    }

    public int getMaxHealth() { return _maxHealth; }
    public int getCurrentHealth() { return _curHealth; }

    public void Damage(int dmg) {
        _curHealth -= dmg;
    }

    public void Heal(int heal) {
        _curHealth = _curHealth + heal < _maxHealth
            ? _curHealth + heal
            : _maxHealth;
    }
}
