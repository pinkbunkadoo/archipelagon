using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindIndicator : MonoBehaviour {
	// public Material mat;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<MeshFilter>();
		// gameObject.AddComponent<MeshRenderer>();
		
		Mesh mesh = new Mesh();
		mesh.vertices = new Vector3[] { new Vector3(0f, 0.5f, 0f), new Vector3(0.5f, -0.5f, 0f), new Vector3(0f, 0f, 0f), new Vector3(-0.5f, -0.5f, 0f) };
		mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
		mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0.5f, 0.5f), new Vector2(1, 1) };
		mesh.RecalculateNormals();
		// wind.transform.position = new Vector3(5.25f, 3.5f, 10f);
		transform.localScale = new Vector3(0.5f, 0.5f, 1);
		GetComponent<MeshFilter>().mesh = mesh;

		// GetComponent<Renderer>().material = mat;
		// wind.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
		GetComponent<Renderer>().material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
		// transform.localRotation = Quaternion.identity;
		// transform.Rotate(0, 0, -Global.windBearing);
		transform.localEulerAngles = new Vector3(0, 0, -Global.windBearing);
	}
}
