using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StepsScrollview : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float speed = 5f;
    Toggle selectedToggle;
    Text toggleText;
    void Start()
    {
        selectedToggle.onValueChanged.AddListener(delegate { ToggleChange(selectedToggle); });
        
    }
    public void ToggleChange(Toggle tglValue)
    {
        toggleText = tglValue.gameObject.transform.GetChild(1).GetComponent<Text>();
        if (tglValue.isOn)
        {
            //toggleText.color = Color.green;
            
        }
        else
        {
            toggleText.color = Color.green;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
       
    }
}
