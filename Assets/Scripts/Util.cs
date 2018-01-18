using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    // public static float SignedAngleTo(Vector3 a, Vector3 b) {
    //     return Mathf.Atan2(a.x * b.y - a.y * b.x, a.x * b.x + a.y * b.y) * Mathf.Rad2Deg;
    // }

	public static float AngleSigned(float ax, float ay, float bx, float by) {
		return Mathf.Atan2(ax * by - ay * bx, ax * bx + ay * by) * Mathf.Rad2Deg;
	}

}
