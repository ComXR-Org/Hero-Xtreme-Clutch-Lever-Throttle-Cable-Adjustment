using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotateByTouchInput : MonoBehaviour
{
    [SerializeField] GameObject ObjectToRoate;
    private Touch touch;
    private Vector2 touchposition;
    private Quaternion rotationY;
    private float roationSpeed = 0.1f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * roationSpeed, 0f);
                ObjectToRoate.transform.rotation = rotationY * ObjectToRoate.transform.rotation;
            }
        }
    }
}
