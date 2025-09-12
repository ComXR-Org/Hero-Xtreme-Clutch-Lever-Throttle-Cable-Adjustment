using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollHandler : MonoBehaviour
{
    public RectTransform content;
    public Scrollbar scrollBar;
    public int numberOfItems;
    public int currentStep;
    [SerializeField]private Steps stepScript;

    // Start is called before the first frame update
    void Start()
    {
        stepScript = FindObjectOfType<Steps>();
        currentStep = 0;
    }

    public void SetScrollPosNext() {

        currentStep = stepScript.currentStep;
        numberOfItems = stepScript.steps.Count;
        if (currentStep > 2) {
            content.position += new Vector3(0, 25, 0);
        }    
    }

    public void SetScrollPosPrev() {

        currentStep = stepScript.currentStep;
        numberOfItems = stepScript.steps.Count;
        if (currentStep > 2) {
            content.position -= new Vector3(0, 25, 0);
        }
    }


}
