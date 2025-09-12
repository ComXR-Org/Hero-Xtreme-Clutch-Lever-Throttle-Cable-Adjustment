using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveObjectSelection : MonoBehaviour
{
    [Space(10)]
    [Header("A Tool to Load and Save Selections in Named Groups")]
    [Space(20)]
    public  MySelection[] selections;
    // Start is called before the first frame update
    [Space(10)]
    string s;
    // Update is called once per frame
    [MenuItem("Tools/ Add Selection Manager GameObject")]
    static void AddSelectionManagerToScene()
    {
        GameObject selectionManager = new GameObject();
        selectionManager.transform.position = Vector3.zero;
        selectionManager.name = "Selection Manager";
        selectionManager.AddComponent<SaveObjectSelection>();
    }
    [System.Serializable]
    public class MySelection
    {
        public string name;
        public GameObject[] selection;
    }
}

