using UnityEngine;
using Serialization;

public class MapController : MonoBehaviour {
    public static string NameMap = "";

    [SerializeField] private SaveLevel _saveLevel;

    void Start () {
	    if (NameMap != "") {
            _saveLevel.Load(NameMap);
	    }
	}
}
