using UnityEngine;
using UnityEngine.UI;

public class GetTextMultiLanguages : MonoBehaviour {
    [SerializeField] private Text _t;
    [SerializeField] private string _key;

    private void Start() {
        _t.text = Global.JsonReader.ReadValue(_key);
    }
}