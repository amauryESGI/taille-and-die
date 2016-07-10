using UnityEngine;

public static class SaveConfig {
    public static string dataPath;

    static SaveConfig() {
        dataPath = Application.dataPath;
        dataPath = dataPath.Replace("Assets", "");
    }
}