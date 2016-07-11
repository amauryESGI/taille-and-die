using System;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField]
    private GameObject _listObject;
    [SerializeField]
    private GameObject _prefabSpawner;
    [SerializeField]
    private GameObject _prefabPlayerCharacter;
    [SerializeField]
    private FollowSmoothlyTarget _Camera;

    // Use this for initialization
    void Start() {
        foreach (Transform child in _listObject.transform) {
            if (string.Compare(child.name.Replace("(Clone)", ""), _prefabSpawner.name, StringComparison.Ordinal) == 0) {
                GameObject go = Instantiate(_prefabPlayerCharacter);
                go.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + 2);
                _Camera.Player = go;
            }
        }
    }
}
