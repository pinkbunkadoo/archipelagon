using UnityEngine;
using UnityEditor;
using System.Collections;

class Placement : EditorWindow {

    GameObject groundObject;
    string myString = "";
    bool normalToggle = true;

    [MenuItem ("Window/Placement")]
    static void Init()
    {
        EditorWindow window = GetWindow(typeof(Placement));
        window.Show();
    }
    // public static void  ShowWindow () {
    //     EditorWindow.GetWindow(typeof(Placement));
	// 	// EditorGUI.ColorField();
    // }
    
    void OnGUI () {
        // The actual window code goes here
		// EditorGUILayout.ColorField("New Color:", matColor);
        
        GUIStyle customGuiStyle = new GUIStyle();
        customGuiStyle.wordWrap = true;
        // customGuiStyle.padding = new RectOffset(8, 8, 8, 8);

        // EditorGUILayout.BeginArea(new Rect(0, 0, 1000, 1000), customGuiStyle);
        // EditorGUILayout.BeginVertical(customGuiStyle);
        EditorGUILayout.LabelField("Select one or more objects to place onto a chosen ground mesh:", customGuiStyle);

        normalToggle = GUILayout.Toggle(normalToggle, "Normal:");

		if (GUILayout.Button("Ground Mesh (" + myString + ")")) SelectGround();
        if (GUILayout.Button("Place Selected")) Place();
        // EditorGUILayout.EndVertical();

        // EditorGUILayout.EndArea();
        
        this.Repaint();
        

    }

	void SelectGround() {
		if (Selection.activeGameObject) {
            myString = Selection.activeGameObject.name;
            groundObject = Selection.activeGameObject;
            // this.Repaint();
		}
	}

    void Place() {
        // RaycastHit[] hits;
        // hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);
    	foreach(GameObject go in Selection.gameObjects) {
            if (go != groundObject) {
                RaycastHit hit;
                if (Physics.Raycast(go.transform.position, -Vector3.up, out hit, 1000.0f)) {
                    if (hit.transform == groundObject.transform) {
                        go.transform.position = hit.point;
                        // go.transform.right = hit.normal;
                        go.transform.rotation = Quaternion.FromToRotation(go.transform.up, hit.normal);
                    }
                }
            }
        }
    }

}