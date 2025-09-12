using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class GameObjectHelper : EditorWindow {

    public Object root;
    public static string[] layers, tags;
    public int selectedLayer = 0, selectedTag = 0;

    [MenuItem("Helper/Toggle Gameobjects by Tag")]
    static void Init() {
        EditorWindow window = GetWindow(typeof(GameObjectHelper));
        window.minSize = new Vector2(300f, 200f);
        window.maxSize = new Vector2(300f, 200f);
        window.Show();
        layers = GetLayers();
        tags = GetTags();
    }

    public static string[] GetTags() {
        return UnityEditorInternal.InternalEditorUtility.tags;
    }

    public static string[] GetLayers() {
        List<string> layerNames = new List<string>();
        for (int i = 0; i < 31; i++) {
            string layer = LayerMask.LayerToName(i);
            if (layer.Length > 0) {
                layerNames.Add(layer);
            }
        }
        return layerNames.ToArray();
    }

    void OnGUI() {
        GUILayout.Space(10f);
        root = EditorGUILayout.ObjectField("Root", root, typeof(GameObject));
        GUILayout.Space(10f);
        selectedTag = EditorGUILayout.Popup("Selected Tag", selectedTag, tags);
        if (GUILayout.Button("Enable Gameobjects")) {
            ToggleState(true);
        }
        if (GUILayout.Button("Disable Gameobjects")) {
            ToggleState(false);
        }
        GUILayout.Space(10f);
        if (GUILayout.Button("Enable Parts (Dismantling)")) {
            ToggleDismantlingParts(true);
        }
        if (GUILayout.Button("Disable Parts (Dismantling)")) {
            ToggleDismantlingParts(false);
        }
        GUILayout.Space(10f);
        if (GUILayout.Button("Enable Parts (Assembly)")) {
            ToggleAssemblyParts(true);
        }
        if (GUILayout.Button("Disable Parts (Assembly)")) {
            ToggleAssemblyParts(false);
        }
    }

    void ToggleState(bool enableObject) {
        if (root != null) {
            string tagName = tags[selectedTag];
            int i = 0;
            GameObject r = root as GameObject;
            Transform[] tr = r.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in tr) {
                if (t.tag == tagName) {
                    t.gameObject.SetActive(enableObject);
                    i++;
                    if (t.childCount > 0) {
                        foreach (Transform child in t) {
                            child.gameObject.SetActive(enableObject);
                            i++;
                        }
                    }
                }
            }
            Debug.Log((enableObject ? "Enabled " : "Disabled ") + i + " objects");
        }
        else {
            Debug.Log("Root is null");
        }
    }

    void ToggleDismantlingParts(bool enableObject) {
        if (root != null) {
            GameObject r = root as GameObject;
            foreach (Step s in r.GetComponent<Steps>().steps) {
                foreach (GameObject g in s.objsToDisable) {
                    g.SetActive(enableObject);
                }
            }
        }
        else {
            Debug.Log("Root is null");
        }
    }

    void ToggleAssemblyParts(bool enableObject) {
        if (root != null) {
            GameObject r = root as GameObject;
            foreach (Step s in r.GetComponent<Steps>().assemblySteps) {
                foreach (GameObject g in s.objsToEnable) {
                    g.SetActive(enableObject);
                }
            }
        }
        else {
            Debug.Log("Root is null");
        }
    }
}
