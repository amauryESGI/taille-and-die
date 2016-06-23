using UnityEngine;
using System.Collections;

public class LoadPrefab : MonoBehaviour {
    [SerializeField]
    GameObject prefablToClone;
    GameObject clone;
    bool isDragging = false, isRotating = false;
    Vector3 oldMousePos;
    // Use this for initialization
    void Start () {

	}

    public void Onclick() {
        isDragging = true;
        clone = GameObject.Instantiate(prefablToClone);
        clone.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void ClampObjectToGrid() {
        int x = (int)clone.transform.position.x;
        int y = (int)clone.transform.position.y;
        Debug.Log(x + " :: " + y);
        clone.transform.position = new Vector3(x, y, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.x /= 25;
            mousePos.y -= Screen.height / 2;
            mousePos.y /= 25;
            clone.transform.position = mousePos;
            if (Input.GetMouseButtonDown(1))
            {
                isDragging = false;
                ClampObjectToGrid();
                clone = null;
            }
            if (Input.GetMouseButtonDown(0))
            {
                oldMousePos = mousePos;
                isDragging = false;
                isRotating = true;
                ClampObjectToGrid();
            }
        }
        if (isRotating)
        {
            
        }
	}
}
