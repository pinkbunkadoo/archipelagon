using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraController : MonoBehaviour {
	private float y;

	// Use this for initialization
	void Start () {
		y = transform.position.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		GameObject glider = GameObject.Find("Glider");
		transform.position = new Vector3(glider.transform.position.x, y, glider.transform.position.z);
		// transform.position.y = y;
	}
}
