using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;


[RequireComponent(typeof(Renderer))]
public class AddRemoveOutline : MonoBehaviour
{
    public bool enableOutline;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Outline>() == null)
        {
            gameObject.AddComponent<Outline>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enableOutline)
        {
            if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().enabled=true;
            }
            else
            {
                gameObject.AddComponent<Outline>();
            }
        }
        else
        {
            if (gameObject.GetComponent<Outline>() != null)
            {
                gameObject.GetComponent<Outline>().enabled = false;
            }
        }
        
    }
}
