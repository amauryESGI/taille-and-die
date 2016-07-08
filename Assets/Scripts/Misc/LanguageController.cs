using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

public class LanguageController : MonoBehaviour, INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    private static string _currentLanguage;

    public string CurrentLanguage {
        get { return _currentLanguage; }
        set {
            _currentLanguage = value;
            PlayerPrefs.SetString("CurrentLanguage", _currentLanguage);
            RaisePropertyChanged("CurrentLanguage");
        }
    }

    public string GetCurrentLanguage() {
        return CurrentLanguage;
    }

    void Awake() {
        CheckLanguage();
        Global.LanguageController = this;
    }

    public void CheckLanguage() {
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
    }
    
    public void RaisePropertyChanged(string propertyName) {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
}
