using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float bound = 50.0f;
	public float speed = 5.0f;


	float minFo = 1f, maxFo = 20f, sensi = 5f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		//move x
		if(Input.mousePosition.x > Screen.width - bound || Input.GetKey( KeyCode.RightArrow ))
			transform.position = new Vector3 (this.transform.position.x + (speed + Camera.main.orthographicSize) * Time.deltaTime, transform.position.y, transform.position.z);
		if(Input.mousePosition.x < bound || Input.GetKey( KeyCode.LeftArrow ))
			transform.position = new Vector3 (this.transform.position.x - (speed + Camera.main.orthographicSize) * Time.deltaTime, transform.position.y, transform.position.z);

		//move z
		if(Input.mousePosition.y > Screen.height - bound || Input.GetKey( KeyCode.UpArrow ))
			transform.position = new Vector3 (this.transform.position.x, transform.position.y + (speed + Camera.main.orthographicSize) * Time.deltaTime, transform.position.z);
		if(Input.mousePosition.y < bound || Input.GetKey( KeyCode.DownArrow ))
			transform.position = new Vector3 (this.transform.position.x, transform.position.y - (speed + Camera.main.orthographicSize) * Time.deltaTime, transform.position.z);

		float camF = Camera.main.orthographicSize;
		camF += Input.GetAxis ("Mouse ScrollWheel") * sensi;
		camF = Mathf.Clamp (camF, minFo, maxFo);
		Camera.main.orthographicSize = camF;
	}
}
