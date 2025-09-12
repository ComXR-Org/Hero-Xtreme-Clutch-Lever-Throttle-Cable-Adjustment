using IndieMarc.CurvedLine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    public GameObject startingPoint;
    public GameObject midPoint_node;
    public GameObject endPoint;

    [Space(10)]
    public GameObject wire;

    GameObject[] finalPath;

    public bool drawGizmos = false;
    bool nodeFound = false;
    bool endPointFound = false;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    [ContextMenu("Create Wire")]
    public void CreateWire() {
        finalPath = new GameObject[0];
        nodeFound = false;
        endPointFound = false;

        List<GameObject> gos = new List<GameObject>();
        gos.Add(startingPoint);
        MapWire(gos, startingPoint);
    }

    void MapWire(List<GameObject> path, GameObject nextPoint) {
        // get all the connected points for that gameobject
        GameObject[] objs = nextPoint.GetComponent<ConnectedObjects>().linkedObjects;
        for (int i = 0; i < objs.Length && !endPointFound; i++) {
            // a condition to make sure that it is not reversed when it reaches the end
            if (!path.Contains(objs[i])) {
                List<GameObject> newPath = new List<GameObject>();
                newPath.AddRange(path);
                newPath.Add(objs[i]);

                GameObject currentTarget = midPoint_node;
                if (nodeFound) {
                    currentTarget = endPoint;
                }

                // check if the selected object is a node
                if (objs[i] == currentTarget) {
                    // update the flag so the search is stopped
                    if (!nodeFound) {
                        nodeFound = true;
                        MapWire(newPath, objs[i]);
                        continue;
                    }
                    else {
                        endPointFound = true;
                    }
                    
                    PrintPath("Path: ", newPath);
                    finalPath = newPath.ToArray();
                    Transform[] transArray = newPath.Select(f => f.transform).ToArray();
                    wire.GetComponent<CurvedLine3D>().paths = transArray;
                    wire.GetComponent<CurvedLine3D>().Refresh();
                    return;
                }
                else {
                    // the current object is not a node. Continue searching.
                    MapWire(newPath, objs[i]);
                }
            }
            else {
                // same object is found. Probably reached the end.
                //PrintPath("Reached End: ", path);
            }
        }
    }

    void PrintPath(string prefix, List<GameObject> gos) {
        string s = prefix;
        foreach (GameObject g in gos) {
            s += g.name + " => ";
        }
        s = s.TrimEnd(new char[] { ' ', '=', '>' });
        Debug.Log(s);
    }

    void OnDrawGizmos() {
        float gizmoSize = 0.03f;
        if (drawGizmos) {
            if (finalPath.Length > 2) {
                for (int i = 0; i < finalPath.Length - 1; i++) {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(finalPath[i].transform.position + new Vector3(0f, 0.01f, 0f), finalPath[i + 1].transform.position + new Vector3(0f, 0.01f, 0f));
                }
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(startingPoint.transform.position, gizmoSize);
                Gizmos.DrawSphere(endPoint.transform.position, gizmoSize);
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(midPoint_node.transform.position, gizmoSize);
            }
        }
    }
}