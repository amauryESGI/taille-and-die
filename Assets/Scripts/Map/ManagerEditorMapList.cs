using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine.SceneManagement;

public class ManagerEditorMapList : MonoBehaviour {
    //
    [SerializeField]
    private string NameSceneLoad;
    [SerializeField]
    private ManagedEditorMapList _scrollListManager;
    private string[] _mapList;

    void Start() {
        SearchMap();
    }

    public void SearchMap() {
        // Clear the list of map
        _scrollListManager.RemoveAll();
        MasterServer.ClearHostList();

        // The list of map has been retrieved .
        _mapList = Directory.GetFiles(SaveConfig.MapPath); // TODO : filter .xml

        // We process information to be displayed
        var items = new List<ItemInformationMap>();
        for (var i = 0; i < _mapList.Length; i++) {
            items.Add(new ItemInformationMap {
                Name = Path.GetFileNameWithoutExtension(_mapList[i]),
                //DateTime = File.GetLastAccessTime(_mapList[i]).ToString("MM/dd/yyyy HH:mm:ss"),
                DoWork = LoadMap
            });
        }

        _scrollListManager.PopulatePanel(items);
    }

    public void LoadMap(string name) {
        MapController.NameMap = name;
        SceneManager.LoadScene(NameSceneLoad);
    }
}