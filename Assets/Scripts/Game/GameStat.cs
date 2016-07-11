using UnityEngine;

public class GameStat : MonoBehaviour {
    [SerializeField]
    private MapController _mapController;

    void Start() {
        if (MapController.NameMap != string.Empty) {
            string keyNbPlay = MapController.NameMap + "NbTry";
            if (PlayerPrefs.HasKey(keyNbPlay)) {
                PlayerPrefs.SetInt(keyNbPlay, PlayerPrefs.GetInt(keyNbPlay) + 1);
            } else {
                PlayerPrefs.SetInt(keyNbPlay, 1);
            }
        }
    }

    public static void Win() {
        string keyNbWin = MapController.NameMap + "NbWin";
        if (PlayerPrefs.HasKey(keyNbWin)) {
            PlayerPrefs.SetInt(keyNbWin, PlayerPrefs.GetInt(keyNbWin) + 1);
        } else {
            PlayerPrefs.SetInt(keyNbWin, 1);
        }
    }
}
