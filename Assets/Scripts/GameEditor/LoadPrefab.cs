using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LoadPrefab : MonoBehaviour {
    [SerializeField]
    GameObject prefablToClone;
	[SerializeField]
	GameObject ListObj;
    GameObject clone;

	static typeObject actualObj;
	[SerializeField]
	typeObject numObject = 0;


	[SerializeField]
	int limit = -1;
	int actual_number = 0;

	bool isDragging = false, isRotating = false, wantDestroy = false;
    Vector3 oldMousePos;


    // Use this for initialization
    void Start () {

	}

    public void Onclick() {
		if (actual_number != limit) {
			LoadPrefab.actualObj = numObject;
			if(numObject != typeObject.destroy)
			{
				isDragging = true;
				clone = GameObject.Instantiate (prefablToClone);
				clone.transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			}
		}
			
    }

    void ClampObjectToGrid() {
        int x = (int)clone.transform.position.x;
        int y = (int)clone.transform.position.y;
		clone.transform.position = new Vector3(Mathf.Floor(clone.transform.position.x)+0.5f, Mathf.Floor(clone.transform.position.y)+0.5f, 0);
		clone.transform.parent = ListObj.transform;
		actual_number++;

		if (actual_number == limit) {
			this.transform.parent.gameObject.SetActive (false);
		}
			
		//gol.Add(clone);
    }

	// Update is called once per frame
	void Update () {
		if (LoadPrefab.actualObj == numObject) {
			if (numObject != typeObject.destroy) {
				if (actual_number == limit) {
					isDragging = false;
				}
				if (isDragging) {
					Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					mousePos.z = 0;
					clone.transform.position = new Vector3 (mousePos.x - 0.5f, mousePos.y - 0.5f, 0);
					if (Input.GetMouseButtonDown (1)) {
						isDragging = false;
						//ClampObjectToGrid();
						Destroy (clone);
					}
					if (Input.GetMouseButton (0)) {
						oldMousePos = mousePos;
						isRotating = true;
						ClampObjectToGrid ();
						Onclick ();
					}
				}
				if (isRotating) {
					
				}
			} else {
				if(Input.GetMouseButton (0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
					Debug.Log ("test");
					if(hit.transform != null)
					{
						Debug.Log ("test2");
						if(hit.transform.parent != null)
						{
							
							if(hit.transform.parent.transform.parent != null && hit.transform.parent.parent.name ==  "objectList")
							{
								Destroy(hit.transform.gameObject);
							}

						}
					}

				}
			}

		} else {
			Destroy (clone);
		}
	}
}
