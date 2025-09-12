using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateParentPos : MonoBehaviour
{
    Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        transform.root.position += prevPos - transform.localPosition;
        transform.position += prevPos - transform.localPosition;
        //prevPos = transform.localPosition;
    }
}
