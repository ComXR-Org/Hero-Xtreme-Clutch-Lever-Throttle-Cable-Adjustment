using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batching : MonoBehaviour {

    GameObject[] carparts;

    // Start is called before the first frame update
    void Start() {
        MeshRenderer[] mrs = this.transform.GetComponentsInChildren<MeshRenderer>(true);
        List<GameObject> gbs = new List<GameObject>();
        foreach (MeshRenderer m in mrs) {
            gbs.Add(m.gameObject);
        }
        carparts = gbs.ToArray();
        Batch();
    }

    // Update is called once per frame
    void Update() {
    }

    void Batch() {
        StaticBatchingUtility.Combine(carparts, this.transform.gameObject);
    }
}
