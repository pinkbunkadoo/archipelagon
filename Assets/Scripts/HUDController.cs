using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {
	public GameObject markerPrefab;
	public GameObject unidentifiedPrefab;

	private GameObject target;
	private GameObject marker;

	// public GameObject gliderBlip;
	// public Material gliderBlipMaterial;

	// static float t = 0.0f;
	// static float size = 40;
	// private Camera camera;
	private Beacon[] beacons;
	private Camera camera;
	private GameObject player;

	class Beacon {
		public GameObject go;
		public GameObject marker;

		public Beacon(GameObject go, GameObject marker) {
			// print("beacon " + marker.layer);
			this.go = go;
			this.marker = marker;
		}
	}

	void SetMarker(GameObject go) {
		target = GameObject.Find("Seagulls");
		marker = Instantiate(markerPrefab);
	}

	void UpdateMarker() {
		// Camera camera = GetComponent<Camera>();
		// if (target != null) {
		Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
		Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);
		// worldPos.y += 0.5f;
		worldPos.z = 20;
		marker.transform.position = worldPos;

		float d = Vector3.Distance(Camera.main.transform.position, target.transform.position);

		if (d > 200) {
			marker.SetActive(false);
		} else {
			marker.SetActive(true);
		}
	}


	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		camera = GetComponent<Camera>();
		// camera = Camera.main;
		var wind = GameObject.Find("HUD/Wind");
		var map = GameObject.Find("HUD/Map");
		map.transform.position = new Vector3(-camera.orthographicSize * camera.aspect + 1.8f, -camera.orthographicSize + 1.8f, 0);
		wind.transform.position = new Vector3(+camera.orthographicSize * camera.aspect - 1, -camera.orthographicSize + 1, 0);

		SetMarker(null);

		GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
		beacons = new Beacon[items.Length];

		for (int i = 0; i < items.Length; i++) {
			// print(item.name);
			GameObject go = Instantiate(unidentifiedPrefab, transform);
			go.layer = transform.gameObject.layer;
			go.transform.position = new Vector3(0, 0, 1);
			go.transform.localScale = new Vector3(0.8f, 0.8f, 1);
			// go.transform.Rotate(Vector3.right, -90);

			beacons[i] = new Beacon(items[i], go);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null)
			UpdateMarker();

		Vector3 v1 = Camera.main.WorldToScreenPoint(player.transform.position);

		foreach (var beacon in beacons) {
			// var p = Camera.main.WorldToViewportPoint(beacon.go.transform.position);

			Vector3 screenPos = Camera.main.WorldToScreenPoint(beacon.go.transform.position);
			screenPos.y += 20;
			Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);

			worldPos.z = 1;
			beacon.marker.transform.position = worldPos;

			var p1 = new Vector3(v1.x, v1.y, 0);
			var p2 = new Vector3(screenPos.x, screenPos.y, 0);

			// Vector3 v1 = Camera.main.WorldToScreenPoint(player.transform.position);
			// Vector3 v2 = Camera.main.WorldToScreenPoint(beacon.go.transform.position);

			var d = Vector3.Distance(p1, p2);

			float f = Mathf.Clamp(d / 300, 0, 1);
			float s = Mathf.Clamp(1 - f, 0f, 1);
			// beacon.marker.transform.localScale = new Vector3(s, s, s);

			
			// material.SetFloat("_Transparency", f);
			var renderer = beacon.marker.GetComponent<Renderer>();
			if (d > 300) {
				renderer.enabled = false;
			} else {
				renderer.enabled = true;
			}

			Global.Display("Text4", beacon.go.name + " " + d.ToString("n2"));

			// beacon.marker.transform.position = p;
		}

	}

	void LateUpdate() {

	}
}
