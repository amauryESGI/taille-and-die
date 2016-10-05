using UnityEngine;
using System.Collections.Generic;


public class LoadPrefab : MonoBehaviour {
    private static int IdDestroy = 1;

    [SerializeField]
    private SampleButtonUIEditor _sampleButton;

    public string Name { get { return _sampleButton.Prefab.name; } }
    public int Id { get; private set; }
    private static int _idCounter = 0;
    private static int _idCurrentObjectSelected = 0;

    GameObject clone;

    static List<Vector2> vecList = new List<Vector2>();

    private int _currentNumberOfObject = 0;

    bool isDragging = false;
    Vector3 oldMousePos;
    public int NumberOfObject {
        get { return _currentNumberOfObject; }
        set {
            _currentNumberOfObject = value;

            if (_sampleButton != null)
                this.transform.parent.gameObject.SetActive(_currentNumberOfObject != _sampleButton.LimNumberObject);
        }
    }

    void Awake() {
        Id = System.Threading.Interlocked.Increment(ref _idCounter);
        if (_sampleButton.Prefab == null && _sampleButton.PrefabName.text == "Destroy")
            IdDestroy = Id;
    }

    public void Onclick() {
        if (NumberOfObject != _sampleButton.LimNumberObject) {
            _idCurrentObjectSelected = Id;
            if (_idCurrentObjectSelected != IdDestroy) { // different de l'objet destroy
                isDragging = true;
                clone = GameObject.Instantiate(_sampleButton.Prefab);
                clone.name = Name;
                clone.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    void ClampObjectToGrid() {
        var vec2 = new Vector2(Mathf.Floor(clone.transform.position.x) + 0.5f, Mathf.Floor(clone.transform.position.y) + 0.5f);

        if (!vecList.Contains(vec2)) {
            clone.transform.position = new Vector3(vec2.x, vec2.y, 0);
            clone.transform.parent = _sampleButton.GameObjectListOnMap.transform;
            NumberOfObject++;
            vecList.Add(clone.transform.position);
            if (clone.GetComponent<Rigidbody2D>() != null)
                clone.GetComponent<Rigidbody2D>().isKinematic = true;

            Onclick();
        }
    }

    void Update() {
        if (_idCurrentObjectSelected == Id) {
            if (_idCurrentObjectSelected != IdDestroy) { // different de l'objet destroy
                if (_currentNumberOfObject == _sampleButton.LimNumberObject) {
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
                        //TODO : decrementer NumberOfObject
                        //NumberOfObject--;
                        vecList.Remove(hit.transform.position); // ??
                        Destroy(hit.transform.parent.transform.gameObject); // ??
                    }
                }
            }
        } else {
            Destroy(clone);
        }
    }
}
