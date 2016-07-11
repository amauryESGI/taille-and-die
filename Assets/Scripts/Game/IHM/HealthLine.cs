using UnityEngine;
using System.Collections.Generic;

public class HealthLine : MonoBehaviour {
    [SerializeField] private GameObject ItemHearth;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Transform _contentPanel ;

    public Health PlayerHealth {
        get { return _playerHealth; }
        set {
            _playerHealth = value;
            Init();
        }
    }

    [SerializeField]
    private List<Hearth> _healthLine = new List<Hearth>();

    void Start() {
        if (_playerHealth != null)
            Init();
    }

    void Update() {
        while (_healthLine.Count < _playerHealth.getMaxHealth())
            _popHearth();

        for (var i = 0; i < _playerHealth.getCurrentHealth(); i++)
            _healthLine[i].IsUp = true;

        for (var i = _playerHealth.getCurrentHealth(); i < _playerHealth.getMaxHealth(); i++)
            _healthLine[i].IsUp = false;
    }

    private void Init() {
        for (int i = 0; i < _playerHealth.getMaxHealth(); i++)
            _popHearth();
    }

    private void _popHearth() {
        var hearth = Instantiate(ItemHearth).GetComponent<Hearth>();
        hearth.IsUp = true;
        hearth.transform.SetParent(_contentPanel, false);

        _healthLine.Add(hearth);
    }
}