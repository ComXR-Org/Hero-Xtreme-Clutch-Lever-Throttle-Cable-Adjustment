using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 public class FontChanger : MonoBehaviour {
 
     private Text[] GetText;
     public Font myFont;
 
     // Use this for initialization
     void Start () {
         GetText = Text.FindObjectsOfType<Text> (true);
 
         foreach (Text go in GetText)
             go.font = myFont;
     }
 }