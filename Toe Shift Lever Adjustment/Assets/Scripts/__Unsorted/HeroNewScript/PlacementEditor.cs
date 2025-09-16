using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementEditor : MonoBehaviour
{
    public Slider scaleSlider;
    public Slider rotSlider;
    public GameObject car;

    public List<Slider> moveList;

    public Toggle move;
    public Toggle rotate;
    public Toggle scale;

    public GameObject EditPanel;

    private float previousValue;
    private float prevPosX;
    private float prevPosY;
    private float prevPosZ;
    // Start is called before the first frame update
    void Start()
    {
        previousValue = rotSlider.value;
        prevPosX = moveList[0].value;
        prevPosY = moveList[1].value;
        prevPosZ = moveList[2].value;
    }

    // Update is called once per frame
    void Update()
    {
        if (car == null) {
            car = GameObject.FindGameObjectWithTag("Car");
        }
    }

    public void Scaling() {

        car.transform.localScale = new Vector3(scaleSlider.value *2, scaleSlider.value *2, scaleSlider.value *2);
    }

    public void Rotate() {

        float delta = rotSlider.value - previousValue;
        car.transform.Rotate(Vector3.up * delta * 360);

        previousValue = rotSlider.value;
    }

    public void Movement() {

        float deltaX = moveList[0].value - prevPosX;
        float deltaY = moveList[1].value - prevPosY;
        float deltaZ = moveList[2].value - prevPosZ;
        car.transform.localPosition += new Vector3(deltaX *5, 0.0f, 0.0f);
        car.transform.localPosition += new Vector3(0.0f, deltaY * 5, 0.0f);
        car.transform.localPosition += new Vector3(0.0f, 0.0f, deltaZ * 5);

        prevPosX = moveList[0].value;
        prevPosY = moveList[1].value;
        prevPosZ = moveList[2].value;
    }

    public void EditPanelSwitch() {

        if (EditPanel.activeSelf == true)
            EditPanel.SetActive(false);
        else
            EditPanel.SetActive(true);
    }

    public void MoveToggle() {

        moveList[0].gameObject.SetActive(true);
        moveList[1].gameObject.SetActive(true);
        moveList[2].gameObject.SetActive(true);
        rotSlider.gameObject.SetActive(false);
        scaleSlider.gameObject.SetActive(false);
    }

    public void RotateToggle() {

        rotSlider.gameObject.SetActive(true);
        moveList[0].gameObject.SetActive(false);
        moveList[1].gameObject.SetActive(false);
        moveList[2].gameObject.SetActive(false);
        scaleSlider.gameObject.SetActive(false);
    }

    public void ScaleToggle() {

        scaleSlider.gameObject.SetActive(true);
        moveList[0].gameObject.SetActive(false);
        moveList[1].gameObject.SetActive(false);
        moveList[2].gameObject.SetActive(false);
        rotSlider.gameObject.SetActive(false);
    }

    public void ResetToDefault() {

        if (move.isOn) {
            foreach(Slider s in moveList) {
                s.value = 0.0f;
            }
        }
        else if (rotate.isOn) {
            rotSlider.value = 0f;
        }

        else if (scale.isOn) {
            scaleSlider.value = 0.5f;
        }
    }
}
