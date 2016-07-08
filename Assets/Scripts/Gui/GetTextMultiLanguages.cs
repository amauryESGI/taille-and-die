using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class GetTextMultiLanguages : MonoBehaviour {
    [SerializeField] private Text _t;
    [SerializeField] private string _key;

    private void Start() {
        _t.text = Global.JsonReader.ReadValue(_key);
        Global.LanguageController.PropertyChanged += languageController_PropertyChanged;
    }

    private void languageController_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        _t.text = Global.JsonReader.ReadValue(_key);
    }
}