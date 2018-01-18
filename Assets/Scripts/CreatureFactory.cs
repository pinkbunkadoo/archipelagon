using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureFactory : MonoBehaviour {
	public static int SEAGULL = 0;
	
	public GameObject[] prefabs;

	public GameObject Create(int type) {
		try {
			GameObject prefab = prefabs[type];
			var instance = Instantiate(prefab, new Vector3(0, 100, 0), Quaternion.identity);
			instance.transform.SetParent(transform);
			return instance;
		} catch(System.Exception e) {
			print(e);
		}
		return null;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
