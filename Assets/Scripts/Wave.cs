using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour {
	public int state = 0;
	private float t = 0;
	private Vector3 scale;

	public void Go () {
		if (state == 0) {
			state = 1;
			t = 0;
			float x = Random.Range(-500, 500);
			float y = 0.5f;
			float z = Random.Range(-500, 500);
			transform.position = new Vector3(x, y, z);
			
			float s = 10 + Random.value * 20;
			transform.localScale = new Vector3(s, s, s);

			scale = transform.localScale;
		}
	}
	
	// Use this for initialization
	void Start () {
		scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 1) {
			float s = t * 2;
			if (t > 0.5f) s = (1 - t) * 2;

			transform.localScale = new Vector3(scale.x, 1 + scale.y * s, scale.z);

			t += 0.2f * Time.deltaTime;

			if (t > 1) {
				state = 0;
				t = 0;
			}
		}
	}
}
