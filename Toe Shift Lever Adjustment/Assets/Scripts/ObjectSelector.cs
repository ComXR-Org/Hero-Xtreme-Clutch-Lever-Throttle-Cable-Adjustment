using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Modules {
    public string connectedObject;
    public string moduleName; // FuelTank_DIY or Speedometer_Info
    public int moduleID;
    public bool isDIY = false; //if true show Info icon else DIY icon
}

public class ObjectSelector : MonoBehaviour {
    [SerializeField] List<Modules> modules;
    [SerializeField] float size = 100f;
    [SerializeField] GameObject moduleBuntton, partInfoButton;
    [SerializeField] GameObject parentSpawner;
    //[SerializeField] string selectedObjectName;
    void Update() {
        GetInfo();
    }
    public  GameObject[] allChildren;
    void GetInfo() {
        
        if (Input.GetMouseButtonDown(0) && Input.touchCount ==1) {

            if(parentSpawner.transform.childCount > 0) {
                GameObject[] allChildren = new GameObject[parentSpawner.transform.childCount];
                //Debug.Log(allChildren.Length);
                for (int i = 0; i < parentSpawner.transform.childCount; i++) {
                    allChildren[i] = parentSpawner.transform.GetChild(i).gameObject;
                }
                foreach (GameObject child in allChildren) {
                    Destroy(child.gameObject);
                }
            }
            Debug.Log("Clicked");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f)) {
                CreateButtonList(hit.transform);
            }
        }
    }

    void CreateButtonList(Transform hit) 
    {
        foreach (Modules m in modules) {
            if (m.connectedObject == hit.name) {
                if (m.moduleName != "") 
                {
                    Debug.Log(m.moduleName);
                    ObjectToInstantiate(moduleBuntton, m.moduleName);
                }
            }
        }
    }

    void ObjectToInstantiate(GameObject g, string name) {
        GameObject instantiated = Instantiate(g, transform.position, transform.rotation);
        instantiated.transform.parent = parentSpawner.transform;
        instantiated.GetComponentInChildren<TMP_Text>().text = name;
    }
}
