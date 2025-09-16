using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleDetailContainers : MonoBehaviour
{
    public GameObject mainParent;
    [SerializeField]List<GameObject> containers;

  
    public void resetContainer(TMP_Text activeContainer)
    {
        foreach (GameObject cont in containers)
        {
            cont.SetActive(false);
        }
     
       
        foreach (GameObject cont in containers)
        {
            if (cont.gameObject.name.Contains(activeContainer.text))
            {
                cont.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(mainParent.GetComponent<RectTransform>());
            }
        }
    }

}
