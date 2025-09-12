using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateText : MonoBehaviour {

    public TextMeshPro scaleValue;
    public string value;
    public string resetValue;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeValue() {
        scaleValue.text = value;
    }

    public void ResetValue() {
        scaleValue.text = resetValue;
    }
}
