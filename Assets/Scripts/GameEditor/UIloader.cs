using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    #region Public Methods
    public void ShowHideList() {
        Vector3 tmp = transform.position;
        tmp.x -= transform.position.x * 2;
        GameObject go = GameObject.FindGameObjectWithTag("MaskUI");
        go.GetComponent<Text>().text = go.GetComponent<Text>().text == "+" ? "-" : "+";
        transform.position = tmp;
    }
    #endregion
    #region Private Methods
    void LoadAllPrefab() {

    }

    void LoadGrid() { }
    #endregion
}
