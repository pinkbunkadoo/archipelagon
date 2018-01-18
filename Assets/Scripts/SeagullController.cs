using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour {
	private float dir;
	private float speed;
	private float turnSpeed;

	// Use this for initialization
	void Start () {
		dir = Random.Range(-1f, 1f);
		dir = dir < 0 ? Mathf.Floor(dir) : Mathf.Ceil(dir);
		speed = Mathf.Ceil(Random.Range(2f, 4f));
		turnSpeed = speed + Mathf.Ceil(Random.value * 50);
		// print(dir);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * speed;
		transform.Rotate(0, dir * turnSpeed * Time.deltaTime, 0);
	}
}
