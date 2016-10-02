using UnityEngine;
using UnityEngine.UI;

public class UIloader : MonoBehaviour {
    #region Public Methods
    public void ShowHideList() {
        Vector3 tmp = transform.position;
        tmp.x -= transform.position.x * 2;
        GameObject go = GameObject.FindGameObjectWithTag("MaskUI");
        go.GetComponent<Text>().text = go.GetComponent<Text>().text == "+" ? "-" : "+";
        transform.position = tmp;
    }
    #endregion
}
