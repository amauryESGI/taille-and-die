using UnityEngine;

public class Global : MonoBehaviour {
    public static JsonReader JsonReader;
    public static string CurrentLanguage;

    private void Awake() {
        CheckLanguage();
    }

    private static void CheckLanguage() {
        if (PlayerPrefs.HasKey("CurrentLanguage")) {
            CurrentLanguage = PlayerPrefs.GetString("CurrentLanguage");
        }
        else {
            CurrentLanguage = SelectLanguages(Application.systemLanguage);
            PlayerPrefs.SetString("CurrentLanguage", CurrentLanguage);
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
}
