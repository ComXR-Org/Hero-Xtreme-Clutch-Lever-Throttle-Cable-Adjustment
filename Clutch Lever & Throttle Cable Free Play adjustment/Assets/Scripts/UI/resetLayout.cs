using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resetLayout : MonoBehaviour
{
    int childCpount;
    [SerializeField] GameObject Container;
    private void Start()
    {
        childCpount = this.gameObject.transform.childCount;
    }
    void Update()
    {
        if (childCpount != this.gameObject.transform.childCount)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Container.GetComponent<RectTransform>());
            childCpount = this.gameObject.transform.childCount;
        }
      
    }
}
