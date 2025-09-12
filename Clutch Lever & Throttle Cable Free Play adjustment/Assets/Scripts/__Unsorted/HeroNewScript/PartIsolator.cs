using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartIsolator : MonoBehaviour
{
    public List<GameObject> parts;
    public GameObject resetButton;
    public PartSelectionUI partUI;

    private void OnEnable() {
        resetButton = GameObject.Find("Button_Reset");
        partUI = GameObject.FindObjectOfType<PartSelectionUI>();
        //resetButton.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        IsolateSelectedPart();

    }

    public void Reset() {
        foreach(GameObject part in parts) {

            part.SetActive(true);
            //resetButton.SetActive(false);
        }
        partUI.PartUIInActive();
    }

    void IsolateSelectedPart() {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200)) {

                foreach (GameObject part in parts) {


                    if (hit.transform.parent.name == part.name || hit.transform.gameObject.name == part.name || hit.transform.parent.parent.name == part.name) {
                        part.SetActive(true);
                        partUI.partName.text = part.name;
                    } else
                        part.SetActive(false);
                }
                partUI.PartUIActive();
            }
        }
    }
}
