using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliderBlipController : MonoBehaviour {
	static float t = 0.0f;
	static float size = 0.2f;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>();
		
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[] { new Vector3(0f, 0.5f, 0f), new Vector3(0.5f, 0f, 0f), new Vector3(-0.5f, 0f, 0f) };
		mesh.triangles = new int[] { 0, 1, 2 };
		mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
		mesh.RecalculateNormals();
		// wind.transform.position = new Vector3(5.25f, 3.5f, 10f);
		transform.localScale = new Vector3(size, size, 1);
		GetComponent<MeshFilter>().mesh = mesh;
		GetComponent<Renderer>().material.color = Color.white;		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t >= 0.5) {
			gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
			t = 0;
		}
		GameObject glider = GameObject.Find("Glider");
		transform.localEulerAngles = new Vector3(0, 0, -glider.transform.localEulerAngles.y);
	}
}
