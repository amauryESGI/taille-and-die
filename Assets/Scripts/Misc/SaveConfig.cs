using System.IO;
using UnityEngine;

public static class SaveConfig {
    public static string DataPath;
    public static string MapPath;

    static SaveConfig() {
        DataPath = Application.dataPath;
        DataPath = DataPath.Replace("Assets", "");
        MapPath = DataPath + "Map";

        if (!Directory.Exists(MapPath))
            Directory.CreateDirectory(MapPath);
    }
}