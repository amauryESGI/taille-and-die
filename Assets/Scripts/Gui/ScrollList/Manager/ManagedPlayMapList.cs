using System.Collections.Generic;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

public class ManagedPlayMapList : ManagedScrollList<ItemInformationMap> {
    [SerializeField]
    private GameObject _sampleButton;

    protected override void _populateList(List<ItemInformationMap> items) {
        // For each item in list...
        for (var i = 0; i < items.Count; ++i) {
            // We Instantiate a new gameObject and get component for ...
            var button = Instantiate(_sampleButton).GetComponent<SampleButtonMapPlay>();

            // ... initialyze data.
            button.MapName.text = items[i].Name;
            button.NbTry = PlayerPrefs.HasKey(items[i].Name + "NbTry") ? PlayerPrefs.GetInt(items[i].Name + "NbTry") : 0;
            button.NbWin = PlayerPrefs.HasKey(items[i].Name + "NbWin") ? PlayerPrefs.GetInt(items[i].Name + "NbWin") : 0;

            // Here we need copy for does not lost the item at the end of for.
            var item = items[i];
            button.Button.onClick.AddListener(delegate { item.DoWork(button.MapName.text); });

            // In the end, we attache this new gameObject at ContentPanel.
            // We define false for not adapting the child to the parent. 
            button.transform.SetParent(ContentPanel, false);
        }
    }
}