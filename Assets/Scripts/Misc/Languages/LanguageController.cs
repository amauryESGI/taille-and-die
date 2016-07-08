using UnityEngine;

public class LanguageController : MonoBehaviour {
    private static string _currentLanguage;

    public static string CurrentLanguage {
        get { return _currentLanguage; }
        set {
            _currentLanguage = value;
            PlayerPrefs.SetString("CurrentLanguage", _currentLanguage);
        }
    }

    public string GetCurrentLanguage() {
        return CurrentLanguage;
    }

    void Awake() {
        CheckLanguage();
        Global.LanguageController = this;
    }

    public static void CheckLanguage() {
        if (PlayerPrefs.HasKey("CurrentLanguage")) {
            CurrentLanguage = PlayerPrefs.GetString("CurrentLanguage");
        } else {
            CurrentLanguage = SelectLanguages(Application.systemLanguage);
        }
    }
    private static string SelectLanguages(SystemLanguage language) {
        switch (language) {
            case SystemLanguage.French:
                return "fr";
            case SystemLanguage.English:
                return "en";
            default:
                return "en";
        }
    }

    public void ChangeLanguages() {
        if (CurrentLanguage == "en")
            CurrentLanguage = SelectLanguages(Application.systemLanguage);
        else
            CurrentLanguage = "en";

        
        Debug.Log(CurrentLanguage);
    }
}
