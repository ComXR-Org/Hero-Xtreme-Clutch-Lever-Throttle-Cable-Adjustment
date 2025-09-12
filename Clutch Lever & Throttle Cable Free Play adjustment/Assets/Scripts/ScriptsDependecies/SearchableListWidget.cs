using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
[ExecuteInEditMode]
public class SearchableListWidget : MonoBehaviour
{
    public InputField searchBox;
    public RectTransform resultsPanel;
    public SearchableListElement resultElement;
    [System.Serializable]
    public class ListSelectEvent : UnityEvent<int>
    {
    }
    public InputField.OnChangeEvent onSearchTextEdit;
    public ListSelectEvent onListSelect;
    UnityAction btnCallback;
    public List<string> resultList;
    private void Reset()
    {
        searchBox = gameObject.GetComponentInChildren<InputField>();
        onSearchTextEdit = searchBox.onValueChanged;
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        searchBox.onValueChanged.AddListener(OnValueChange);
        TouchScreenKeyboard.hideInput = true;
    }
    void OnValueChange(string newVal)
    {
        onSearchTextEdit.Invoke(newVal);
    }
    public void LoadResults(List<string> results)
    {
        for (int i = 0; i < resultsPanel.childCount; i++)
        {
            if(resultsPanel.GetChild(i)!=null)
            Destroy(resultsPanel.GetChild(i).gameObject);
        }
        for (int i = 0; i < results.Count; i++)
        {
         
            SearchableListElement nBtn = Instantiate(resultElement, resultsPanel);
            nBtn.gameObject.name = nBtn.label.text = results[i];
            int id = i;
             UnityAction clbk = () => ListSelect(id);
            nBtn.button.onClick.AddListener(clbk);
           
        }
    }
    void ListSelect(int id)
    {
        Debug.Log(":" + id);
        onListSelect.Invoke(id);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
