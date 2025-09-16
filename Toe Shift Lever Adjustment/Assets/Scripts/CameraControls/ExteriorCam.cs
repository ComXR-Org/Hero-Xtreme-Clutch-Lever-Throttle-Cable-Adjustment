using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ExteriorCam : MonoBehaviour {

    public Transform target;
    public bool rotateFromCenter = false, camMovement = true;
    public Text fpsCounter;

    [Header("Rotation Speed")]
    public float xSpeed = 0.1f;
    public float ySpeed = 1.0f;

    [Header("Zoom Settings")]
    public float distanceMin = 1f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;

    [Header("Auto Rotation")]
    [SerializeField]
    bool autoRotate = false;
    bool controlCam = true;
    public float autoRotateSpeed = 3;

    [Header("Camera Rotation Limits")]
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;

    [Header("Camera Distance")]
    public float distance = 2.0f;

    public float rotationYAxis = 0.0f;
    public float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    [Header("Pan Settings")]
    [SerializeField] bool panning = false;
    public float panSpeed = 1f;
    public float maxPanRadius = 2f, curRad;
    public bool enableLookAt;
    [Space(10)]
    public Slider zoomDist;

    [SerializeField]
    float orbitPoint = 0;
    [SerializeField] bool touchSupported = false, readingInput = false, isZooming = false, useSmoothing = false;
    float xS, yS, smoothDistance;
    Vector3 prevpos, mousedelta, centerPoint, targetPoint,
        firstPanPoint, secondPanPoint,
        panLastMouseDelta, panTargetMouseDelta, panResetPos;
    Bounds bounds;
    [SerializeField] bool uiInteraction = false;
    public void CallMovement() {
        Vector3 v = new Vector3(0.0f, 0.0f, -distance);

        Vector3 position = Quaternion.Euler(1, 1, 0) * v + target.position;
    }
    // Use this for initialization
    void OnEnable() {
        if (Input.touchSupported) {
            touchSupported = true;
        }
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>()) {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        if (target != null) {
            centerPoint = target.transform.position;
            targetPoint = target.transform.position;
        }

        InvokeRepeating("PrintFPS", 0.5f, 0.5f);
    }

    void PrintFPS() {
        if (fpsCounter)
            fpsCounter.text = "FPS: " + (1 / Time.deltaTime).ToString("00");
    }

    void AutoRotateCam() {
        if (!readingInput) {
            orbitPoint += autoRotateSpeed * Time.deltaTime;
            orbitPoint = (orbitPoint > 359.9) ? 0 : orbitPoint;
            transform.RotateAround(rotateFromCenter ? centerPoint : target.position, Vector3.up, orbitPoint);
        } else {
            orbitPoint = 0;
        }
    }
    bool CheckIfOnUI() {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        bool mouseOverUI = results.Count > 0;
        return mouseOverUI;
    }
    void Update() {
        if (target != null && enableLookAt) {
            Camera.main.transform.LookAt(target);
        }

        uiInteraction = CheckIfOnUI();

        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        if (rotationXAxis > 180f) {
            rotationXAxis -= 360f;
        }
        if (Input.GetMouseButtonDown(0)) {

            prevpos = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            mousedelta = prevpos - Input.mousePosition;
            prevpos = Input.mousePosition;
        }

        GetPanInput();

        /*if (target != null)
        {
            Camera.main.transform.LookAt(target.transform);
        }
        uiInteraction = CheckIfOnUI();

        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        if (rotationXAxis > 180f) {
            rotationXAxis -= 360f;
        }
        if (Input.GetMouseButtonDown(0)) {
          
            prevpos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) {
            mousedelta = prevpos - Input.mousePosition;
            prevpos = Input.mousePosition;
        }
        GetPanInput();*/


    }
    void GetPanInput() {
        if (Input.GetMouseButtonDown(1)) {
            firstPanPoint = Input.mousePosition;
            //  panLastMouseDelta = panTargetMouseDelta;
            panResetPos = transform.localPosition;
        }

        if (Input.GetMouseButton(1)) {
            secondPanPoint = Input.mousePosition;
            Vector3 delta = secondPanPoint - firstPanPoint;
            //   panTargetMouseDelta = panLastMouseDelta + transform.right * delta.x * panSpeed + transform.up * delta.y * panSpeed;
            panTargetMouseDelta = (transform.right * delta.x + transform.up * delta.y) * panSpeed;

        }
    }
    void SmoothReset() {
        //   Debug.Log("SMOOTH");
        float dist = Vector3.Distance(transform.position, nxtPos);
        smoothStat = Time.deltaTime * 2;
        //    smoothStat = Time.deltaTime*(1+ (1-dist)/dist);
        transform.position = Vector3.Lerp(transform.position, nxtPos, smoothStat);
        transform.localRotation = Quaternion.Lerp(transform.rotation, nxtRot, smoothStat);
        if (Vector3.Distance(transform.position, nxtPos) < 0.0025f)
            isSmoothing = false;
        //    distance = Vector3.Distance(transform.position, targetPoint);

    }
    void CamMovement() {
        if (target) {
            if (Input.GetMouseButton(0)) {
                if (controlCam) {
                    velocityX += xSpeed * -mousedelta.x * distance * 0.002f;
                    velocityY += ySpeed * -mousedelta.y * 0.002f;
                }
            }
            //CheckZoomGesture();
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            Vector3 negDistance = Vector3.zero;
            if (zoomDist != null) {
                Camera.main.fieldOfView = zoomDist.value;
            }
            //else {
            negDistance = new Vector3(0.0f, 0.0f, -distance);
            //}
            Vector3 position = rotation * negDistance + target.position;
            //  Vector3 position = rotation * negDistance + targetPoint;


            //if (Vector3.Distance(transform.position, position) > 0.02 || Quaternion.Angle(transform.rotation, rotation) > 5f)

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
# if UNITY_EDITOR
            panning = Input.GetMouseButton(1);
#else
            //panning = Input.touchCount == 2;
            if(Input.touchCount == 2){
                panning = true;
            }
            else if (Input.touchCount == 0) {
                panning = false;
            }
            
#endif

            readingInput = (Input.touchCount > 0 || Input.GetMouseButton(0) || Input.mouseScrollDelta.y != 0 || panning) ? true : false;
            if (autoRotate) {
                AutoRotateCam();
            }
            if (isZooming) {
                distance = Mathf.Lerp(distance, smoothDistance, smoothTime * Time.deltaTime);
            }

            if (readingInput && !uiInteraction) {
                float xMove = Input.GetAxis("Mouse X");
                float yMove = Input.GetAxis("Mouse Y");
                float moveDeadZone = 0.2f;
                if (panning) {
                    enableLookAt = false;
                    //transform.localPosition += new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime, -Input.GetAxis("Mouse Y") * Time.deltaTime, 0);
                    
                    if (xMove > moveDeadZone || xMove < -moveDeadZone || yMove > moveDeadZone || yMove < -moveDeadZone)
                    transform.Translate(-xMove * Time.deltaTime, -yMove * Time.deltaTime, 0);

                    Debug.Log(Input.GetAxis("Mouse X"));

                    //transform.localPosition += panTargetMouseDelta;
                    //  transform.localPosition = Vector3.Lerp(position, transform.localPosition + panTargetMouseDelta, panSpeed * Time.deltaTime);
                    //curRad = Vector3.Distance(position, transform.localPosition);
                    //    if (curRad > maxPanRadius)
                    //      transform.localPosition = Vector3.Lerp(transform.position, panResetPos,Time.deltaTime*2);
                } else {
                    //enableLookAt = true;
#if UNITY_EDITOR
                    transform.position = position;
                    transform.localRotation = rotation;
#endif
                    if (Input.touchCount > 0) {
                        if (Input.GetTouch(0).phase == TouchPhase.Moved) {
                            transform.position = position;
                            transform.localRotation = rotation;
                        }
                    }
                }
            } else
                if (isSmoothing)
                SmoothReset();

        }
    }

    void LateUpdate() {
        //   if(camMovement)
        CamMovement();
    }
    Vector3 pastPos, nxtPos;
    Quaternion pastRot, nxtRot;
    [SerializeField] bool isSmoothing;
    [SerializeField] float smoothStat = 0;
    public void NewPosRot(Vector3 pos, Quaternion rot) {
        pastPos = transform.position;
        pastRot = transform.localRotation;
        nxtPos = pos;
        nxtRot = rot;
        //   distance = Vector3.Distance(pos, targetPoint);
        if (useSmoothing) {
            camMovement = false;
            isSmoothing = true;
            smoothStat = 0;
        } else {
            transform.position = pos;
            transform.localRotation = rot;
            camMovement = true;
        }
    }
    public static float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetDistance(float newDistance) {
        readingInput = true;
        distance = Mathf.Clamp(newDistance, distanceMin, distanceMax);
    }

    public void ToggleAutoRotate() {
        autoRotate = !autoRotate;
    }

    public void StartZoom() {
        isZooming = true;
        controlCam = false;
    }

    public void EndZoom() {
        controlCam = true;
        isZooming = false;
    }

    public void SetZoom(float newDistance) {
        readingInput = false;
        float d = distanceMax - newDistance;
        smoothDistance = Mathf.Clamp(d, distanceMin, distanceMax);
        if (!isZooming) {
            distance = smoothDistance;
        }
    }

    public float prevPinchAmt = 0f;
    public float zoomSpeed = 0.5f;

    void CheckZoomGesture() {
        if (touchSupported) {
            // Pinch to zoom
            if (Input.touchCount == 2) {
                Vector2 touch0 = Input.GetTouch(0).position;
                Vector2 touch1 = Input.GetTouch(1).position;
                float pinchAmt = Vector2.Distance(touch0, touch1);
                if (prevPinchAmt != 0f && Mathf.Abs(pinchAmt - prevPinchAmt) > 2f) {
                    distance -= (pinchAmt - prevPinchAmt) * Time.deltaTime * zoomSpeed;
                }
                else if(prevPinchAmt != 0f && Mathf.Abs(pinchAmt - prevPinchAmt) < 2f) {
                    distance += (pinchAmt - prevPinchAmt) * Time.deltaTime * zoomSpeed;
                }
                prevPinchAmt = pinchAmt;
                distance = Mathf.Clamp(distance, distanceMin, distanceMax);
            }
        } else {
            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distanceMin, distanceMax);
        }
    }
    public float zoomThreshold = 0.2f;
    bool pinchZooming = false;
    void CheckZoomGesture2() {
        //  zoomSpeed = 0.1f;
        if (touchSupported) {
            // Pinch to zoom
            if (Input.touchCount == 2)// && (Input.GetTouch(0).phase==TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved))
            {

                // get current touch positions
                Touch t0 = Input.GetTouch(0);
                Touch t1 = Input.GetTouch(1);
                // get touch position from the previous frame
                Vector2 lastT0 = t0.position - t0.deltaPosition;
                Vector2 lastT1 = t1.position - t1.deltaPosition;
                //float ltctDist = Vector3.Distance((t0.position + t1.position) / 2, (t0.deltaPosition + t1.deltaPosition) / 2);
                //if (ltctDist > panThreshold)
                //{
                //    Debug.Log("Touch Dist: " + ltctDist);
                //panning = true;
                //Pan((t0.position + t1.position) / 2, (lastT0 + lastT1) / 2);
                //}
                //else
                //    panning = false;

                float lastTouchDist = Vector2.Distance(lastT0, lastT1);
                float currTouchDist = Vector2.Distance(t0.position, t1.position);

                // get offset value
                float deltaDistance = lastTouchDist - currTouchDist;
                float deltaTimeSpeed = deltaDistance * Time.deltaTime * zoomSpeed;
                if (Mathf.Abs(deltaDistance) > zoomThreshold) {
                    pinchZooming = true;
                    Debug.Log("Zoom");
                    distance += deltaTimeSpeed;
                } else
                    pinchZooming = false;

                //if (infiniteZoom)
                    //distance = Mathf.Clamp(distance, distance - 0.1f, distanceMax);
                Debug.Log("D: " + distance + "| deltaDist: " + deltaDistance + " | DeltaTS: " + deltaTimeSpeed);
            } else {
                pinchZooming = false;
                if (panning) {
                    panning = false;
                }
            }
        } else {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            //if (infiniteZoom)
                //distance = Mathf.Clamp(distance - scroll, distance - 0.1f, distanceMax);
           //else
                distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel"), distanceMin, distanceMax);
        }
    }

    void Zoom(float deltaMagnitudeDiff, float speed) {
        distance -= deltaMagnitudeDiff * speed;
        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
    }
}