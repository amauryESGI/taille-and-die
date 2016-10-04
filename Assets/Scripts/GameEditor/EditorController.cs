using UnityEngine;

public class EditorController : MonoBehaviour {
    [SerializeField]
    private GameObject _gameObjectListOnMap;
    [SerializeField]
    private GameObject _gameObjectListPrefab;

    void Start() {
        _gameObjectListPrefab.transform.GetComponent<LoadPrefab>();

        foreach (Transform prefab in _gameObjectListPrefab.transform) {
            var lp = prefab.GetComponentInChildren<LoadPrefab>();

            if(lp==null)
                continue;

            int numObject = 0;

            foreach (Transform child in _gameObjectListOnMap.transform) {
                if (child.name == lp.Name + "(Clone)")
                    numObject++;
            }
            lp.NumberOfObject = numObject;
        }
    }
}
