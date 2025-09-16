using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolPopUp : MonoBehaviour
{
    public Steps steps;
    public List<string> toolNames;
    public List<Sprite> toolSprites;
    public RectTransform content;
    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start() {
        steps = FindObjectOfType<Steps>();
        AddToolInfoToList();
        SpawnTools();
    }

    private void SpawnTools() {
        Debug.Log("Spawned");
        for (int i = 0; i < toolNames.Count; i++) {
            GameObject button = Instantiate(buttonPrefab, content);
            button.transform.GetChild(0).GetComponent<Text>().text = toolNames[i];
            button.transform.GetChild(1).GetComponent<Image>().sprite = toolSprites[i];
            
        }
    }

    private void AddToolInfoToList() {
        toolSprites = new List<Sprite>();
        toolNames = new List<string>();
        for (int i = 0; i < steps.assemblySteps.Count; i++) {
            if (steps.steps[i].toolSprite.Length > 0) {
                for (int j = 0; j < steps.steps[i].toolSprite.Length; j++) {
                    if (!toolNames.Contains(steps.steps[i].toolSprite[j].name)) {
                        toolSprites.Add(steps.steps[i].toolSprite[j]);
                        toolNames.Add(steps.steps[i].toolSprite[j].name);
                    }
                    
                }
            }
            if (steps.assemblySteps[i].toolSprite.Length > 0) {
                for (int j = 0; j < steps.assemblySteps[i].toolSprite.Length; j++) {
                    if (!toolNames.Contains(steps.assemblySteps[i].toolSprite[j].name)) {
                        toolSprites.Add(steps.assemblySteps[i].toolSprite[j]);
                        toolNames.Add(steps.assemblySteps[i].toolSprite[j].name);
                    }
                }
            }
        }
    }
}
