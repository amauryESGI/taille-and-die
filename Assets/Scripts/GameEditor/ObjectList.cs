using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ObjectList : MonoBehaviour {
	public List<GameObject> objectList = new List<GameObject> ();
	void Update () {
		Debug.Log (objectList.Count);
	}
}
