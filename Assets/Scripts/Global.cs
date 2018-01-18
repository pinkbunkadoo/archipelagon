using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour {
	public static float windForce;
	public static float windBearing;
	public static float maxAltitude = 500;
	public static float minAltitude = 200;
	public static float minSpeed = 30.0f;
	public static float maxSpeed = 120.0f;

	public GameObject creatureFactory;
	
	private static GameObject canvas;
	private static GameObject player;
	private static GameObject cursor;

	private UnityEngine.Plane plane;
	private bool drag;

	// static float t = 0;

	public static void Display(string name, string value) {
		canvas.GetComponent<Canvas>().SetText(name, value);
	}

	public static void SetMarkerPos(float x, float y) {
		canvas.GetComponent<Canvas>().SetMarkerPos(x, y);
	}

	// Use this for initialization
	void Start () {
		windForce = 0.02f;
		windBearing = Mathf.Floor(Random.value * 360);

		player = GameObject.FindWithTag("Player");
		cursor = GameObject.FindWithTag("Cursor");
		canvas = GameObject.FindWithTag("Canvas");

		var seagull = creatureFactory.GetComponent<CreatureFactory>().Create(CreatureFactory.SEAGULL);
		seagull.transform.position = new Vector3(0, 100, 0);

		plane = new UnityEngine.Plane(Vector3.up, Vector3.up * player.transform.position.y);
	}

	void UpdateWaypoint() {
		float distance;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (plane.Raycast(ray, out distance)) {
			Vector3 pos = ray.GetPoint(distance);
			player.GetComponent<Player>().SetWayPoint(pos);

			SetMarkerPos(Input.mousePosition.x, Input.mousePosition.y);
			// Display("Text4", Input.mousePosition.x.ToString());

		}
	}
	
	// Update is called once per frame
	void Update () {

		plane.SetNormalAndPosition(Vector3.up, Vector3.up * player.transform.position.y);

		if (Input.GetMouseButtonUp(0)) {
			player.GetComponent<Player>().SetBoost(0);
		}

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
            if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.CompareTag("Item")) {
					print("item");
					cursor.GetComponent<Cursor>().SetPosition(hit.transform.position);
				}
				else if (hit.transform.CompareTag("Airstrip")) {
					print("airstrip");
				}
				else if (hit.transform.CompareTag("Player")) {
					print("boom");
				}
				else {
					UpdateWaypoint();
				}
			}
		} else {
			if (Input.GetMouseButton(0)) {
				Vector3 view = Camera.main.ScreenToViewportPoint(Input.mousePosition);
				float fx = Mathf.Abs(view.x - 0.5f) * 2;
				float fy = Mathf.Abs(view.y - 0.5f) * 2;
				float boost = fx > fy ? fx : fy;

				player.GetComponent<Player>().SetBoost(10);

				float mx = Input.GetAxis("Mouse X");
				float my = Input.GetAxis("Mouse Y");

				if (mx != 0 || my != 0) {
					UpdateWaypoint();
				}
			}
		}
		// t += Time.deltaTime;

		// if (t >= 2.0) {
		// 	float r = Random.value;
		// 	if (r > 0.9) {
		// 		windBearing = Mathf.Floor(Random.value * 360);
		// 		windForce = 0.02f + Random.value / 50;
		// 	}
		// 	t = 0;
		// }
	}

	void LateUpdate() {
		// plane.transform.position = player.transform.position;
		// plane.SetNormalAndPosition(Vector3.up, player.transform.position);
	}
}
