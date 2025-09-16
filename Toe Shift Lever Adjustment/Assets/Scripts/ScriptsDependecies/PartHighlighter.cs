using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using EPOOutline;

public class PartHighlighter : MonoBehaviour {
    //public Outline sample;

    // public FullScreenObjectCamera camera;
    public List<GameObject> list;


    [Header("TEST")]
    [SerializeField] bool test = false;
    public string[] highlightNames;
    public List<Outlinable> curList;
    [SerializeField] bool next = false;
    [SerializeField] int cur = 0;
    public int totalHighlights = 0;
    public void Highlight(string[] parts) {
        if (curList != null)
            if (curList.Count > 0)
                RemoveHighLight();
        curList = new List<Outlinable>();
        for (int i = 0; i < parts.Length; i++) {
            GameObject g = GameObject.Find(parts[i]);
            Highlight(g);
        }
    }

    public void RemoveAllHighlights() {
        Outlinable[] allH = FindObjectsOfType<Outlinable>();
        foreach (var item in allH) {
            item.enabled = false;
        }
        Debug.Log("Removed ALL highlights");
        totalHighlights = 0;
    }

    void AddToListAndHighlight(Outlinable r) {
        curList.Add(r);
        AddOutLineTo(r.gameObject);
    }

    void AddOutLineTo(GameObject g) {
        Outlinable h = g.GetComponent<Outlinable>();
        if (h == null) {
            Debug.LogError("Highlighter not added for: " + g.name);
            totalHighlights++;
        }
        h.enabled = true;
        //if (sample)
        //h.color = sample.color;
    }

    public void Highlight(GameObject[] gos) {
        foreach (var item in gos) Highlight(item);
    }

    public void Highlight(GameObject g) {
        if (g != null) {
            //if (g.transform.childCount >= 1) {
            //    int total = g.transform.childCount;
            //    List<Renderer> children = new List<Renderer>(g.GetComponentsInChildren<Renderer>());
            //    foreach (var item in children) {
            //        AddToListAndHighlight(item.gameObject);
            //    }
            //}
            //else {
            AddToListAndHighlight(g);
            //}
        }
    }

    void AddToListAndHighlight(GameObject g) {
        if (g.GetComponent<Outlinable>()) {
            curList.Add(g.GetComponent<Outlinable>());
            AddOutLineTo(g);
        }
    }

    public void RemoveHighlightFor(GameObject[] arr) {
        for (int i = 0; i < arr.Length; i++) {
            if (arr[i].GetComponent<Outlinable>())
                arr[i].GetComponent<Outlinable>().enabled = false;
        }
        totalHighlights -= arr.Length;
    }

    public void RemoveHighLight() {

        for (int i = 0; i < curList.Count; i++) {
            curList[i].GetComponent<Outlinable>().enabled = false;
        }
        totalHighlights -= curList.Count;
        curList = new List<Outlinable>();
        if (totalHighlights > 0)
            RemoveAllHighlights();
    }

    // Start is called before the first frame update
    void Start() {
        if (test)
            if (highlightNames.Length >= 1)
                Highlight(highlightNames);
    }

    // Update is called once per frame
    void Update() {
        if (next) {
            cur++;
            if (cur >= highlightNames.Length)
                cur = 0;
            Highlight(new string[] { highlightNames[cur] });
            next = false;
        }
    }
}
