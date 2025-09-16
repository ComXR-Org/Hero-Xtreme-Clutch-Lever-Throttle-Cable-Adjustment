using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SaveObjectSelection))]
[CanEditMultipleObjects]

public class SaveObjectSelectionEditor : Editor
{
    private static readonly string[] _dontIncludeMe = new string[] { "m_Script", "Selection" };
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        OnBeforeDefaultInspector();
        DrawPropertiesExcluding(serializedObject, GetInvisibleInDefaultInspector());
        OnAfterDefaultInspector();
        serializedObject.ApplyModifiedProperties();
        //  DrawDefaultInspector();
        SaveObjectSelection targetPlayer = (SaveObjectSelection)target;

 
        //    if (GUILayout.Button("Load Selection"))
        //    {
        if (targetPlayer.selections.Length>0)
        {
            for (int i = 0; i < targetPlayer.selections.Length; i++)
            {
                if (GUILayout.Button("Select " + targetPlayer.selections[i].name))
                    Selection.objects = targetPlayer.selections[i].selection;
            }
        }

    }
    protected virtual void OnBeforeDefaultInspector()
    { }

    protected virtual void OnAfterDefaultInspector()
    { }

    protected virtual string[] GetInvisibleInDefaultInspector()
    {
        return _dontIncludeMe;
    }

}




