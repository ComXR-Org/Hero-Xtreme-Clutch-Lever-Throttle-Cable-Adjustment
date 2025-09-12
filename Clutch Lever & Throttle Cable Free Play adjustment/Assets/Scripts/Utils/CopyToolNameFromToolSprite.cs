using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyToolNameFromToolSprite : MonoBehaviour
{
    [SerializeField] public Steps main;

    void Awake()
    {

        if (main == null)
            main = FindObjectOfType<Steps>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Copy dismental toolnames from sprites")]
    public void CopyToolNamesFromSprite_Dism()
    {
        List<Step> result = new List<Step>();
        List<Step> stl = main.steps;
        for (int s = 0; s < stl.Count; s++)
        {

            Step curStep = stl[s];


            string allToolNames = "";
            if (curStep.toolSprite != null)
            {
                if (curStep.toolSprite.Length > 0)
                {
                    for (int t = 0; t < curStep.toolSprite.Length; t++)
                    {
                        if (curStep.toolSprite[t] != null)
                            allToolNames += curStep.toolSprite[t].name;
                        else
                            allToolNames += "(?)";
                        allToolNames += ":;:";
                    }
                    Debug.Log(curStep.toolSprite.Length + " tool names updated for step " + curStep.debugStepNum + " " + curStep.locateObjectText + "having toolname: " + allToolNames);
                }
                else
                {
                    Debug.Log("No tools for step " + curStep.debugStepNum);
                }
            }
            else
            {
                Debug.Log("ToolSpriteNull for step " + curStep.debugStepNum);
            }
            curStep.specialToolName = allToolNames;
            result.Add(curStep);
        }
        Debug.Log("CopyToolNamesFromSprite_Dism completed");
        main.steps = result;
    }
    [ContextMenu("Copy assembly toolnames from sprites")]
    public void CopyToolNamesFromSprite_Asm()
    {
        List<Step> result = new List<Step>();
        List<Step> stl = main.assemblySteps;
        for (int s = 0; s < stl.Count; s++)
        {

            Step curStep = stl[s];


            string allToolNames = "";
            if (curStep.toolSprite != null)
            {
                if (curStep.toolSprite.Length > 0)
                {
                    for (int t = 0; t < curStep.toolSprite.Length; t++)
                    {
                        if (curStep.toolSprite[t] != null)
                            allToolNames += curStep.toolSprite[t].name;
                        else
                            allToolNames += "(?)";

                        allToolNames += ":;:";
                    }
                    Debug.Log(curStep.toolSprite.Length + " tool names updated for step " + curStep.debugStepNum + " "+curStep.locateObjectText+"having toolname: " + allToolNames);
                }
                else
                {
                    Debug.Log("No tools for step " + curStep.debugStepNum);
                }
            }
            else
            {
                Debug.Log("<color=red>ToolSpriteNull for step</color> " + curStep.debugStepNum);
            }
            curStep.specialToolName = allToolNames;
            result.Add(curStep);
        }
        Debug.Log("CopyToolNamesFromSprite_Asm completed");
        main.assemblySteps = result;
    }
}
