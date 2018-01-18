using UnityEngine;
using UnityEditor;
using System.Collections;

class VertexColorEditor : EditorWindow {

	Color matColor = Color.white;

    [MenuItem ("Window/Vertex Color Editor")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(VertexColorEditor));
		// EditorGUI.ColorField();
    }
    
    void OnGUI () {
        // The actual window code goes here
		matColor = EditorGUILayout.ColorField("New Color:", matColor);
		if (GUILayout.Button("Grab")) GrabColor();
		if (GUILayout.Button("Change!")) ChangeColors();
    }

	void GrabColor() {
		// Debug.Log(Selection.activeGameObject);
		var mf = Selection.activeGameObject.GetComponent<MeshFilter>();
		var color = mf.sharedMesh.colors[0];
		matColor = color;
	}

	void ChangeColors() {
		if (Selection.activeGameObject) {
			foreach(GameObject t in Selection.gameObjects) {
				// var rend = t.GetComponent<Renderer>();
				// if (rend != null) rend.sharedMaterial.color = matColor;
				var mf = t.GetComponent<MeshFilter>();
				if (mf) {
					var mesh = Instantiate(mf.sharedMesh);
					if (mesh != null) {
						Vector3[] vertices = mesh.vertices;
						Color[] colors = new Color[vertices.Length];
						
						for (int i = 0; i < vertices.Length; i++)
							colors[i] = matColor;

						// assign the array of colors to the Mesh.
						mesh.colors = colors;
						mesh.name = "Mesh";
						t.GetComponent<MeshFilter>().mesh = mesh;
					}
					
				}
			}
		}
	}
}