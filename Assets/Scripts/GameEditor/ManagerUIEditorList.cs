using System.IO;
using System.Collections.Generic;
using Assets.Scripts.Gui.ScrollList.Manager;
using Assets.Scripts.GUI.ScrollList.Item;
using SimpleJSON;
using UnityEngine;

public class ManagerUIEditorList : MonoBehaviour {
    [SerializeField]
    private ManagedUIEditor _scrollListManager;

    private JSONNode _json;

    void Start() {
        SearchEditorItem();
    }

    public void SearchEditorItem() {
        // Clear the list of map
        _scrollListManager.RemoveAll();

        var dirItemList = new List<string>();
        // The list of item has been retrieved.
        foreach (string path in Directory.GetDirectories(SaveConfig.EditorPath))
            dirItemList.Add("Editor/" + (new DirectoryInfo(path)).Name);

        // We process information to be displayed.
        var items = new List<ItemInformationPrefabUIEditor>();

        for (var i = 0; i < dirItemList.Count; i++) {
            if (!GetJsonFile(dirItemList[i] + "/settings"))
                continue;

            items.Add(new ItemInformationPrefabUIEditor {
                Name = ReadValue("Name"),
                PathPrefab = "Prefab/" + ReadValue("PrefabName"),
                PathPreview = "Sprit/" + ReadValue("PreviewName"),
                LimiteNumberObject = int.Parse(ReadValue("LimNumberObject"))
            });
        }

        _scrollListManager.PopulatePanel(items);
    }

    public string ReadValue(string key) {
        if (_json == null || _json[key] == null)
            return "UNKNOW";

        return _json[key].Value;
    }

    private bool GetJsonFile(string path) {
        _json = null;
        _json = JSON.Parse(Resources.Load(path, typeof(object)).ToString());

        return _json != null;
    }
}
