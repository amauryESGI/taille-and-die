using UnityEngine;
using UnityEngine.UI;

public class SampleButtonMapPlay : MonoBehaviour {
    public Button Button;
    public Text MapName;

    [SerializeField]
    private Text TryVsWin;

    private int _nbTry, _nbWin;

    public int NbTry {
        get { return _nbTry; }
        set {
            _nbTry = value;
            TryVsWin.text = _nbTry + " / " + NbWin;
        }
    }

    public int NbWin {
        get { return _nbWin; }
        set {
            _nbWin = value;
            TryVsWin.text = NbTry + " / " + _nbWin;
        }
    }
}