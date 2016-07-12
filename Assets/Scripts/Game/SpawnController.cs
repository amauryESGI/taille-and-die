using System;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField]
    private GameObject _listObject;
    [SerializeField]
    private GameObject _prefabSpawner;
    [SerializeField]
    private GameObject _prefabEnemies;
    [SerializeField]
    private GameObject _prefabPlayerCharacter;
    [SerializeField]
    private FollowSmoothlyTarget _Camera;
    [SerializeField]
    private HealthLine _hudHealthLine;

    // Use this for initialization
    void Start() {
        GameObject player = null;
        foreach (Transform child in _listObject.transform) {
            if (string.Compare(child.name.Replace("(Clone)", ""), _prefabSpawner.name, StringComparison.Ordinal) == 0) {
                player = Instantiate(_prefabPlayerCharacter);
                player.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + 2);
                _Camera.Player = player;
                _hudHealthLine.PlayerHealth = player.GetComponent<Health>();
            }
        }
        foreach (Transform child2 in _listObject.transform) {
            if (string.Compare(child2.name.Replace("(Clone)", ""), _prefabEnemies.name, StringComparison.Ordinal) == 0) {
                var iaInterpretor = child2.GetComponentInChildren<IAInterpretator>();
                if (player != null)
                    iaInterpretor.Player = player;
            }
        }
    }
}