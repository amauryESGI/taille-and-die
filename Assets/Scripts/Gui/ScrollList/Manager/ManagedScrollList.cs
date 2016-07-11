using System.Collections.Generic;
using UnityEngine;

public abstract class ManagedScrollList<T> : MonoBehaviour {
    [SerializeField]
    protected List<T> ItemList;
    [SerializeField]
    protected Transform ContentPanel;

    protected virtual void ClearPanel() {
        // For each child in ContentPanel...
        for (var i = 0; i < ContentPanel.transform.childCount; i++) {
            // We destroy it.
            Destroy(ContentPanel.transform.GetChild(i).gameObject);
        }
    }

    public void RemoveAll() {
        ItemList.Clear();
        ClearPanel();
    }

    public bool RemoveItem(T items) {
        return ItemList.Remove(items);
    }

    protected abstract void _populateList(List<T> items);

    public void PopulatePanel(IEnumerable<T> items) {
        ItemList.AddRange(items);
        _populateList(items as List<T>);
    }

    public void RefreshPanel() {
        ClearPanel();
        _populateList(ItemList);
    }
}