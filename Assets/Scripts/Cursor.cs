using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
	private float t = 0;
	private float s = 40;

	public void SetPosition(Vector3 position) {
		transform.position = position;
		transform.localScale = new Vector3(s, s, s);
		t = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float f = t * 40;
		transform.localScale = new Vector3(s + f, s + f, s + f);
		// transform.localScale = new Vector3(s + f, s + f, s + f)
		
		// GetComponent<Renderer>().material.SetFloat("_Transparency", t);
		
		t += 1 * Time.deltaTime;

		if (t > 1.0) {
			t = 0;
		}
	}
}
