using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaTrimController : MonoBehaviour {
	private Material mat;
	private float cutoff = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Material mat = GetComponent<Renderer>().material;
		cutoff = Mathf.PingPong(Time.time, 3f);
		mat.SetFloat("_Cutoff", (0.5f + (cutoff / 8)));
	}
}
