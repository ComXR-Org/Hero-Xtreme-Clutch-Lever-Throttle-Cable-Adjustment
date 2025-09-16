using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Main : MonoBehaviour
{
    private Stack<GameObject> panelStack = new Stack<GameObject>();

    void Start()
    {
        //Find the initial panel and open it
       GameObject initialPanel = GameObject.Find("MainPanel");
        OpenPanel(initialPanel);
    }

    public void OpenPanel(GameObject panel)
    {
        // Set the panel to active
        panel.SetActive(true);

        // Push the panel onto the stack
        panelStack.Push(panel);
    }

    public void ClosePanel()
    {
        // Pop the current panel from the stack
        GameObject currentPanel = panelStack.Pop();

        // Set the current panel to inactive
        currentPanel.SetActive(false);

        // Get the previous panel from the stack
        GameObject previousPanel = panelStack.Peek();

        // Set the previous panel to active
        previousPanel.SetActive(true);
    }
}
