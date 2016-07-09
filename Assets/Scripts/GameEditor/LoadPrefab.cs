using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LoadPrefab : MonoBehaviour {
    [SerializeField]
    GameObject prefablToClone;
	[SerializeField]
	GameObject ListObj;
    GameObject clone;

	bool isDragging = false, isRotating = false, wantDestroy = false;
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
		clone.transform.position = new Vector3(Mathf.Floor(clone.transform.position.x)+0.5f, Mathf.Floor(clone.transform.position.y)+0.5f, 0);
		clone.transform.parent = ListObj.transform;
		//gol.Add(clone);
    }

	// Update is called once per frame
	void Update () {
        if (isDragging)
        {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos = Input.mousePosition;
			//mousePos.x += Input.mousePosition.x;
			//mousePos.y += Input.mousePosition.y;
			mousePos.z = 0;
			//mousePos.x += Input.mousePosition.x / Screen.wid;
			clone.transform.position = new Vector3 (mousePos.x-0.5f,mousePos.y-0.5f,0);
            if (Input.GetMouseButtonDown(1))
            {
				isDragging = false;
                //ClampObjectToGrid();
				Destroy(clone);
            }
			if(Input.GetMouseButton(0))
            {
                oldMousePos = mousePos;
                isRotating = true;
                ClampObjectToGrid();
				Onclick ();
            }
        }
        if (isRotating)
        {
            
        }
		if(wantDestroy)
		{

		}
	}
}
