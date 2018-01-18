using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using Archipelago;

public class Player : MonoBehaviour {
	private GameObject body;
	private GameObject trail1;
	private GameObject trail2;
	// private float trailCoolDown = 0.0f;

	// private float altitude = 200.0f;

	private float xRotation = 0.0f;
	private float yRotation = 0.0f;
	private float zRotation = 0.0f;
	
	private float velocity;
	private float acceleration = 0.0f;

	// private GameObject target;
	// private int state;

	private WayPoint waypoint;
	private float angle;
	private float speedCap;
	private float desiredSpeed;
	// private bool hasWaypoint;
	// private float t = 0;


	public class WayPoint {
		public Vector3 position;
		public Vector3 direction;

		public WayPoint(Vector3 position, Vector3 direction) {
			this.position = position;
			this.direction = direction.normalized;
		}
	}

	public void SetWayPoint(Vector3 point) {
		waypoint = new WayPoint(point, point - transform.position);
		angle = Util.AngleSigned(transform.forward.x, transform.forward.z, waypoint.direction.x, waypoint.direction.z);
	}

	public void SetBoost(float factor) {
		acceleration = factor;

		// acceleration = 10 * factor;
		// desiredSpeed = Global.minSpeed + (Global.maxSpeed - Global.minSpeed) * factor;

		// velocity = Global.minSpeed + (Global.maxSpeed - Global.minSpeed) * factor;
		// speedCap = Global.minSpeed + (Global.maxSpeed - Global.minSpeed) * factor;
	}

	void Land(GameObject marker) {
		// state = 1;
		// target = marker;
	}

	void InputFeedback() {
		float yaxis = Input.GetAxis("Vertical");
		float xaxis = Input.GetAxis("Horizontal");

		xRotation += yaxis * Time.deltaTime * 50;
		zRotation += -xaxis * Time.deltaTime * 50;
		yRotation = -zRotation * 4 * Time.deltaTime;

		xRotation = Mathf.Clamp(xRotation, -30, 30);
		zRotation = Mathf.Clamp(zRotation, -30, 30);

		// if (Input.GetKey("space")) {
		// 	acceleration = 10;
		// 	velocity += 10 * Time.deltaTime * acceleration;
		// } else {
		// 	acceleration = 0;
		// 	velocity -= 10 * Time.deltaTime;
		// }

		// velocity += yaxis;

		// if (xaxis == 0) {
		// 	zRotation = Mathf.MoveTowards(zRotation, 0, 40 * Time.deltaTime);
		// }

		// if (yaxis == 0) {
		// 	xRotation = Mathf.MoveTowards(xRotation, 0, 40 * Time.deltaTime);
		// }
	}

	// // Use this for initialization
	void Start() {
		// altitude = transform.position.y;
		velocity = Global.minSpeed;
    	body = GameObject.Find("Glider/Body");
		// waypoint = null;
		// startSpeed = forwardSpeed;
		trail1 = GameObject.Find("Glider/Body/Trail1");
		trail2 = GameObject.Find("Glider/Body/Trail2");
    	// targetRotation = body.transform.localRotation;
		speedCap = Global.maxSpeed;
		desiredSpeed = Global.minSpeed;
	}


	// Update is called once per frame
	void Update () {
		if (waypoint != null) {
			Vector3 direction = waypoint.direction;
			Quaternion rotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);

			float ang = Util.AngleSigned(transform.forward.x, transform.forward.z, direction.x, direction.z);
			float abs = Mathf.Abs(ang);
			float sign = (ang == 0 ? 1 : ang / abs);

			// if (abs > Mathf.Abs(angle) * 0.5) {
			// 	zRotation = Mathf.MoveTowards(zRotation, ang, 10 * Time.deltaTime);
			// } else {
			// 	zRotation = Mathf.MoveTowards(zRotation, 0, 10 * Time.deltaTime);
			// }

			// zRotation = Mathf.Clamp(zRotation, sign < 0 ? -30 : 0, sign < 0 ? 0 : 30);

			zRotation = ang * 0.1f;
			zRotation = Mathf.Clamp(zRotation, sign < 0 ? -30 : 0, sign < 0 ? 0 : 30);

			if (rotation == transform.rotation) {
				transform.rotation = rotation;
				waypoint = null;
				// zRotation = 0;
			}

		} else {
			InputFeedback();
			zRotation = Mathf.MoveTowards(zRotation, 0, 40 * Time.deltaTime);
			xRotation = Mathf.MoveTowards(xRotation, 0, 40 * Time.deltaTime);
		}

		if (acceleration > 0) {
			velocity += 10 * Time.deltaTime * acceleration;
			velocity = Mathf.Clamp(velocity, Global.minSpeed, Global.maxSpeed);
			// acceleration -= 50 * Time.deltaTime;
			// acceleration = Mathf.Clamp(acceleration, 0, acceleration);
		} else {
			// velocity -= 20 * Time.deltaTime;
			// velocity = Mathf.Clamp(velocity, Global.minSpeed, desiredSpeed);
			velocity = Mathf.MoveTowards(velocity, desiredSpeed, 40 * Time.deltaTime);
		}

		transform.position += body.transform.forward * Time.deltaTime * velocity;
		body.transform.localEulerAngles = new Vector3(xRotation, 0, zRotation);
		transform.Rotate(0, yRotation, 0);

		// altitude = transform.position.y;

		trail1.GetComponent<TrailRenderer>().time = 0.5f * ((velocity - Global.minSpeed) / Global.maxSpeed);
		trail2.GetComponent<TrailRenderer>().time = 0.5f * ((velocity - Global.minSpeed) / Global.maxSpeed);

		Global.Display("Text1", transform.forward.ToString());
		Global.Display("Text2", "spd:" + velocity.ToString());
		Global.Display("Text3", "acc:" + acceleration.ToString());
		// Global.Display("Text4", "zrot:" + zRotation.ToString());
		Global.Display("Speed", Mathf.Round(velocity).ToString());
	}

	// void OnTriggerEnter(Collider other) {
	// 	// GameObject go = other.gameObject;
	// 	if (other.CompareTag("Airstrip")) {
	// 		float angle = Vector3.Angle(other.transform.forward, transform.forward);
	// 		// print(angle);
	// 		if (angle < 20 && velocity == Global.minSpeed) {
	// 			GameObject marker = other.transform.GetChild(0).gameObject;
	// 			// Land(marker);
	// 			// print(marker.name);
	// 		}
	// 	}
	// }

	// void OnTriggerStay(Collider other) {
	// 	if (other.gameObject.CompareTag("Draft")) {
	// 		// altitude += Time.deltaTime * Mathf.Pow(1 / transform.position.y, 2) * 100000;
	// 		altitude += Time.deltaTime * 5;
	// 		altitude = Mathf.Clamp(altitude, minAltitude, maxAltitude);
	// 		transform.position = new Vector3(transform.position.x, altitude, transform.position.z);

	// 	}
	// }
}
