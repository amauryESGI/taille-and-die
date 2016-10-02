using UnityEngine;
using System.Collections.Generic;


public class LoadPrefab : MonoBehaviour {
    [SerializeField]
    public GameObject PrefablToClone;
    [SerializeField]
    GameObject ListObj;
    GameObject clone;

    static List<Vector2> vecList = new List<Vector2>();
    static typeObject actualObj;
    [SerializeField]
    typeObject numObject = 0;


    [SerializeField]
    int limit = -1;
    int actual_number = 0;

    bool isDragging = false;
    Vector3 oldMousePos;
    public int NumberOfObject {
        get { return actual_number; }
        set {
            actual_number = value;

            this.transform.parent.gameObject.SetActive(actual_number != limit);
        }
    }

    public void Onclick() {
        if (NumberOfObject != limit) {
            actualObj = numObject;
            if (numObject != typeObject.destroy) {
                isDragging = true;
                clone = GameObject.Instantiate(PrefablToClone);
                clone.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    void ClampObjectToGrid() {
        var vec2 = new Vector2(Mathf.Floor(clone.transform.position.x) + 0.5f, Mathf.Floor(clone.transform.position.y) + 0.5f);

        if (!vecList.Contains(vec2)) {
            clone.transform.position = new Vector3(vec2.x, vec2.y, 0);
            clone.transform.parent = ListObj.transform;
            NumberOfObject++;
            vecList.Add(clone.transform.position);
            if (clone.GetComponent<Rigidbody2D>() != null)
                clone.GetComponent<Rigidbody2D>().isKinematic = true;

            Onclick();
        }
    }

    void Update() {
        if (actualObj == numObject) {
            if (numObject != typeObject.destroy) {
                if (actual_number == limit) {
                    isDragging = false;
                }
                if (isDragging) {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;
                    clone.transform.position = new Vector3(mousePos.x - 0.5f, mousePos.y - 0.5f, 0);
                    if (Input.GetMouseButtonDown(1)) {
                        isDragging = false;
                        Destroy(clone);
                    }
                    if (Input.GetMouseButton(0))
                        ClampObjectToGrid();
                }
            } else {
                if (Input.GetMouseButton(0)) {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit.transform != null
                        && hit.transform.parent != null
                        && hit.transform.parent.transform.parent != null
                        && hit.transform.parent.parent.name == "objectList") {
                        vecList.Remove(hit.transform.position);
                        Destroy(hit.transform.parent.transform.gameObject);
                    }
                }
            }
        } else {
            Destroy(clone);
        }
    }
}
