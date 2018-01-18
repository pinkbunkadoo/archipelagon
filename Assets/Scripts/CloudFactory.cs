using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFactory : MonoBehaviour {
	public int minCount = 20;
	public int maxCount = 40;
	public float altitude = 250.0f;
	public float minSize = 20.0f;
	public float maxSize = 200.0f;
	public Material material;

	public static Dictionary<string, float> lookup;

	private int cloudCount = 0;

	void CreateSegment(float x1, float z1, float x2, float z2, float y) {
		// Vector3[] vertices = {
		// 	new Vector3(x1, y, z1),
		// 	new Vector3(x2, y, z1),
		// 	new Vector3(x2, y, z2),
		// 	new Vector3(x1, y, z2)
		// };
		// int[] triangles = { 0, 1, 2, 0, 2, 3 };

	}

	void CreateCloud(float x, float y, float z) {
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		float width = minSize + (maxSize - minSize) * Random.value;
		float height = minSize + (maxSize - minSize) * Random.value;

		// print(width + " " + height);

		float minX = - width / 2;
		float minZ = - height / 2;
		float maxX = width / 2;
		float maxZ = height / 2;

		Vector3 p = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
		vertices.Add(p);

		float zoff = p.z;
		int vertCount = 1;

		// float inc = minSize / 2;
		// int divisions = (int) (height / inc);
		// inc = minSize;
		float inc = Mathf.Max(minSize, height * 0.25f);

		while (zoff < maxZ) {
			zoff = zoff + inc;
			// zoff = zoff + i;

			// float f = Random.value * (width / 2);
			
			Vector3 p1 = new Vector3(Random.Range(minX, minX + width * 0.4f), 0, zoff);
			Vector3 p2 = new Vector3(Random.Range(maxX - width * 0.4f, maxX), 0, zoff);

			// Vector3 p1 = new Vector3(minX, y, zoff);
			// Vector3 p2 = new Vector3(maxX, y, zoff);

			vertices.Add(p1);
			vertices.Add(p2);
			vertCount += 2;

			triangles.Add(vertCount - 2);
			triangles.Add(vertCount - 1);
			triangles.Add(vertCount - 3);

			if (vertCount > 3) {
				triangles.Add(vertCount - 2);
				triangles.Add(vertCount - 3);
				triangles.Add(vertCount - 4);
			}

			p = p1;
		}

		// Vector3 p = new Vector3(, y, Random.Range(minZ, maxZ));
		vertices.Add(new Vector3(Random.Range(minX, maxX), 0, zoff + inc));
		vertCount++;
		triangles.Add(vertCount - 1);
		triangles.Add(vertCount - 2);
		triangles.Add(vertCount - 3);

		// print(vertices.ToString());
		for (int i = 0; i < vertices.Count; i++) {
			// print(vertices[i]);
		}

		Mesh mesh = new Mesh();
		mesh.name = "Cloud" + cloudCount;
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateBounds();

		GameObject cloud = new GameObject();
		cloud.name = "Cloud" + cloudCount;
		cloud.tag = "Cloud";		
		cloud.transform.position = new Vector3(x, y, z);
		cloud.transform.parent = transform;
		// cloud.layer = gameObject.layer;

		cloud.AddComponent<MeshFilter>();
		cloud.AddComponent<MeshCollider>();
		cloud.AddComponent<MeshRenderer>();

		cloud.GetComponent<MeshFilter>().mesh = mesh;
		cloud.GetComponent<MeshCollider>().sharedMesh = mesh;
		cloud.GetComponent<Renderer>().material = material;
		cloud.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);

		lookup[cloud.name] = 0;
		cloudCount++;
	}

	// Use this for initialization
	void Start () {
		lookup = new Dictionary<string, float>();

		var count = minCount + (maxCount - minCount) * Random.value;
		for (int i = 0; i < count; i ++) {
			CreateCloud(Random.Range(-1000, 1000), altitude + Random.Range(-100, 300), Random.Range(-1000, 1000));
		}
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");

		foreach (GameObject cloud in clouds) {
			Material mat = cloud.GetComponent<Renderer>().material;
			float f = lookup[cloud.name];

			if (f == 0) {
				float alpha = mat.GetFloat("_Transparency");
				alpha = alpha + 1f * Time.deltaTime;
				alpha = Mathf.Clamp(alpha, alpha, 1.0f);
				mat.SetFloat("_Transparency", alpha);
				if (alpha == 1) {
					mat.renderQueue = 2000;
				}
			}

			f -= 4 * Time.deltaTime;
			f = Mathf.Clamp(f, 0, 1);
			lookup[cloud.name] = f;
		}
	}
}
