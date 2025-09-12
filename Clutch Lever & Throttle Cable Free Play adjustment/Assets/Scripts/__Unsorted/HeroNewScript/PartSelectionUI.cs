using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartSelectionUI : MonoBehaviour
{
    public Text partName;
    public GameObject Label;
    public GameObject options;
    public GameObject toggle;

    public void PartUIActive() {
        Label.SetActive(true);
        options.SetActive(true);
        toggle.SetActive(false);
    }

    public void PartUIInActive() {
        Label.SetActive(false);
        options.SetActive(false);
        toggle.SetActive(true);
    }
}
