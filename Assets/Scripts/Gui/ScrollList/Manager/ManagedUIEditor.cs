using System.Collections.Generic;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

namespace Assets.Scripts.Gui.ScrollList.Manager {
    public class ManagedUIEditor : ManagedScrollList<ItemInformationPrefabUIEditor> {
        [SerializeField]
        private GameObject _sampleButton;
        [SerializeField]
        private GameObject _gameObjectListOnMap;

        protected override void _populateList(List<ItemInformationPrefabUIEditor> items) {
            // For each item in list...
            for (var i = 0; i < items.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var button = Instantiate(_sampleButton).GetComponent<SampleButtonUIEditor>();

                // ... initialyze data.
                button.GameObjectListOnMap = _gameObjectListOnMap;
                button.PrefabName.text = items[i].Name;
                Debug.Log(items[i].PathPrefab);
                button.Prefab = Resources.Load(items[i].PathPrefab, typeof(GameObject)) as GameObject;
                button.Preview.sprite = Resources.Load(items[i].PathPreview, typeof(Sprite)) as Sprite;

                // Here we need copy for does not lost the item at the end of for.
                var lp = button.Prefab.GetComponentInChildren<LoadPrefab>();
                button.Button.onClick.AddListener(delegate { lp.Onclick(); });

                button.LimNumberObject = items[i].LimiteNumberObject;

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                button.transform.SetParent(ContentPanel, false);
            }
        }
    }
}
