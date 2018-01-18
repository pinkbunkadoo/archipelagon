using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {
	// private GameObject marker;
	private Transform marker;

	public void SetMarkerPos(float x, float y) {
		marker.position = new Vector3(x, y, 1);
		// print("SetMarkerPos");
	}

	public void SetText(string name, string value) {
		// print(name);
		var text = transform.Find(name);
		if (text) {
			text.GetComponent<Text>().text = value;
		}
		// GameObject.Find("Canvas/" + name).GetComponent<Text>().text = value;
	}

	// Use this for initialization
	void Start () {
		marker = transform.Find("Image");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
