using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour {
	public GameObject prefab;

	private GameObject[] waves;
	private static int count = 100;
	private float t = 0;

	// Use this for initialization
	void Start () {
		waves = new GameObject[count];

		for (int i = 0; i < count; i++) {
			float x = Random.Range(-500, 500);
			float y = 1;
			float z = Random.Range(-500, 500);
			GameObject go = Instantiate(prefab);
			go.transform.position = new Vector3(x, y, z);
			go.transform.parent = transform;
			float s = 50 + Random.value * 10;
			go.transform.localScale = new Vector3(s, 1, s);
			waves[i] = go;
		}
	}
	
	// Update is called once per frame
	void Update () {

		// float s = 10 + Random.value * 10;
		// go.transform.localScale = new Vector3(s, s, s);

		t += 10 * Time.deltaTime;

		if (t >= 1) {
			t = 0;
			int i = (int) Mathf.Ceil(Random.value * count) - 1;
			GameObject go = waves[i];
			go.GetComponent<Wave>().Go();
		}
	}
}
