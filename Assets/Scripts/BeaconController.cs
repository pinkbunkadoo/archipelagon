using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconController : MonoBehaviour {
	private Color colorStart = new Color(1, 1, 1, 0.8f);
	private Color colorEnd = new Color(1, 1, 1, 0);
	// public Color colorCore = Color.red;
	public float duration = 1.0f;

	public GameObject shell;
	// public Material shellMaterial;

	static float t = 0.0f;

	// Use this for initialization
	void Start () {
		shell = GameObject.Find("Beacon/Pulse");
		// shellMaterial = (shell.GetComponent<Renderer>().material);
		// GameObject.Find("Beacon/Mesh").GetComponent<Renderer>().material.color = colorCore;
	}
	
	// Update is called once per frame
	void Update () {
		float lerp = Mathf.Lerp(0f, 1f, t);
        shell.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);

		float scale = Mathf.Lerp(0, 1, t);
		shell.transform.localScale = new Vector3(15 + scale * 50, 15 + scale * 50, 15 + scale * 50);

		// transform.position += new Vector3(1f, 0, 0);

		t += duration * Time.deltaTime;

		if (t > 1.0f) {
            t = 0.0f;
        }
	}
}
