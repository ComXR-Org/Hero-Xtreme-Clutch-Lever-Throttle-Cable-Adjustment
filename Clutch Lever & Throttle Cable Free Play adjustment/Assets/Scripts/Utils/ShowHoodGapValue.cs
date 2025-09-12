using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHoodGapValue : MonoBehaviour
{
 
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hit, Mathf.Infinity))
        {
            //Debug.Log("1");
            if (hit.collider.gameObject.tag == "HoodGaps")
            {
                //Debug.Log("2");

                GameObject g = hit.collider.transform.parent.GetChild(0).gameObject;
                //Debug.Log("3" + g.name);
                if (!g.activeSelf) {
                    g.SetActive(true);
                    StartCoroutine(HideAfterSeconds(g));
                  }
               
            }
           
        }
        
    }
    IEnumerator HideAfterSeconds(GameObject g)
    {
        yield return new WaitForSeconds(3f);
        g.SetActive(false);
    }
}
