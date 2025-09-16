using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
public class OutlineFillColorPulse : MonoBehaviour
{
   [SerializeField] OutlineEffect outlineCam;
    [SerializeField] Color col1, col2;
    [SerializeField] float pulsePerS = 0.7f;
    void Reset()
    {
        Debug.Log("Updating Outline Effect Color Config");
        outlineCam = GetComponent<OutlineEffect>();
        if (outlineCam == null)
            outlineCam = FindObjectOfType<OutlineEffect>();
        if (outlineCam == null)
            outlineCam = Camera.main.gameObject.AddComponent<OutlineEffect>();
        if (outlineCam)
        {
            outlineCam.lineThickness = 1.25f;
            outlineCam.lineIntensity = 1.4f;
                outlineCam.useFillColor = true;
        }
        col1 = new Color(0, 0.55f, 1, 1);
        col2 = new Color(0, 0, 0, 0.25f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (outlineCam)
        {
            outlineCam.fillColor = (Color.Lerp(col1, col2, Mathf.PingPong(Time.time * pulsePerS, 1)));
            outlineCam.lineColor0= (Color.Lerp(col1, col2, Mathf.PingPong(Time.time * pulsePerS, 1)));
        }
    }
}
