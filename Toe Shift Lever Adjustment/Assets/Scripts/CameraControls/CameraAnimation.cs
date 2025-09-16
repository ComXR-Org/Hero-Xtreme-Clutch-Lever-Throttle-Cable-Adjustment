using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraAnimation : MonoBehaviour
{
    Camera main_Camera;
    private void OnEnable()
    {
        main_Camera = Camera.main;
    }

    public void OnVehicleInfo()
    {
        iTween.MoveTo(main_Camera.gameObject, 
            iTween.Hash( "x", 0.17f, "time", 0.2f));
    }
}
