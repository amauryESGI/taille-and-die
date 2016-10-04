using System.IO;
using UnityEngine;

public static class SaveConfig {
    public static string DataPath;
    public static string ResourcesPath;
    public static string MapPath;
    public static string EditorPath;

    static SaveConfig() {
        DataPath = Application.dataPath;
        ResourcesPath = Path.Combine(DataPath, "Resources");
        DataPath = DataPath.Replace("Assets", "");
        MapPath = DataPath + "Map";
        EditorPath = Path.Combine(ResourcesPath, "Editor");

        if (!Directory.Exists(MapPath))
            Directory.CreateDirectory(MapPath);
    }
}