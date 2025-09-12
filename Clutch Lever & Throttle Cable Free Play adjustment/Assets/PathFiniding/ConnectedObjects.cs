using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedObjects : MonoBehaviour {

    public GameObject[] linkedObjects;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnDrawGizmosSelected() {
        if (linkedObjects != null) {
            foreach (GameObject g in linkedObjects) {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, g.transform.position);
            }
        }
    }
}
