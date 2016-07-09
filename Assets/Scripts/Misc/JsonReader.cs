using System.ComponentModel;
using SimpleJSON;
using UnityEngine;

public class JsonReader : MonoBehaviour {
    private JSONNode _json;
    private Object _jsonFile;

    void Awake() {
        Global.JsonReader = this;
    }

    void Start() {
        Global.LanguageController.PropertyChanged += languageController_PropertyChanged;
    }

    private void languageController_PropertyChanged(object sender, PropertyChangedEventArgs e) {
        GetJsonFile();
    }

    public string ReadValue(string key) {
        if (_json == null)
            GetJsonFile();

        if (_json == null || _json[key] == null)
            return "UNKNOW";

        return _json[key].Value;
    }

    private void GetJsonFile() {
        string path = string.Format("strings_{0}", Global.LanguageController.GetCurrentLanguage());
        _jsonFile = Resources.Load(path, typeof(object));
        _json = JSON.Parse(_jsonFile.ToString());
    }
}
