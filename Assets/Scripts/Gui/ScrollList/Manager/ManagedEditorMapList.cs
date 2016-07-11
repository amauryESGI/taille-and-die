using System.Collections.Generic;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

public class ManagedEditorMapList : ManagedScrollList<ItemInformationMap> {
    [SerializeField]
    private GameObject _sampleButton;

    protected override void _populateList(List<ItemInformationMap> items) {
        // For each item in list...
        for (var i = 0; i < items.Count; ++i) {
            // We Instantiate a new gameObject and get component for ...
            var button = Instantiate(_sampleButton).GetComponent<SampleButtonMap>();

            // ... initialyze data.
            button.MapName.text = items[i].Name;
            button.LastModificationDateTime.text = items[i].DateTime;

            // Here we need copy for does not lost the item at the end of for.
            var item = items[i];
            button.Button.onClick.AddListener(delegate { item.DoWork(button.MapName.text); });

            // In the end, we attache this new gameObject at ContentPanel.
            // We define false for not adapting the child to the parent. 
            button.transform.SetParent(ContentPanel, false);
        }
    }
}