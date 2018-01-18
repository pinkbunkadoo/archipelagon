using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

	public GameObject glider;

	private Vector3 offset;
	private float distance;
	private float angle;

	void SetPosition() {
		// transform.position = glider.transform.position;
	}

	// Use this for initialization
	void Start () {
		distance = 75;
		angle = 65;
		transform.position = glider.transform.position;
	}

	void LateUpdate () {
		float m = (glider.transform.position.y / Global.maxAltitude);
		float c = 10 - (m * 10);
		// angle = 60 - c;
		float a = angle - c;
		// angle = 60 + c;

		float y = Mathf.Sin(a * Mathf.Deg2Rad) * distance;
		float z = Mathf.Cos(a * Mathf.Deg2Rad) * distance;
		transform.position = new Vector3(glider.transform.position.x, glider.transform.position.y + y, glider.transform.position.z - z);
		transform.LookAt(glider.transform);

		transform.position += -transform.forward * (c * 4);
	}

	void FixedUpdate() {
 		RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 500.0F);

		for (int i = 0; i < hits.Length; i++) {
			var hit = hits[i];
			if (hit.collider != null) {
				GameObject go = hit.collider.gameObject;
				if (go.CompareTag("Cloud")) {
					// Vector3 p = go.transform.parent.TransformPoint(go.transform.position);
					// print(go.name + " " + go.transform.position.y);
					if (go.transform.position.y > glider.transform.position.y) {
						// print("cloud!");
						var mat = go.GetComponent<Renderer>().material;

						float alpha = mat.GetFloat("_Transparency");
						alpha = alpha - 1 * Time.deltaTime;
						alpha = Mathf.Clamp(alpha, 0.5f, 1.0f);

						mat.SetFloat("_Transparency", alpha);
						CloudFactory.lookup[go.name] = 1;
						mat.renderQueue = 3000;
					}
				}
			}
		}
	}
}
