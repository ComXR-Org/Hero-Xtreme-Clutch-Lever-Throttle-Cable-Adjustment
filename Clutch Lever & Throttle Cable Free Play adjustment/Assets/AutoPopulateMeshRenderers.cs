using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
[CustomEditor(typeof(AnimatedObjectsOfModules))]
public class AutoPopulateMeshRenderers : Editor
{
    private string materialNameToSearch = "";
    private string materialNameToAssign = "";
    private string indicesToUpdate = "";

    private int targetVariantIndex = -1;
    private string variantMaterialName = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AnimatedObjectsOfModules targetScript = (AnimatedObjectsOfModules)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Auto Populate Settings", EditorStyles.boldLabel);

        materialNameToSearch = EditorGUILayout.TextField("Material Name To Match", materialNameToSearch);

        if (GUILayout.Button("Populate meshrenderObject fields"))
        {
            PopulateMatchingRenderers(targetScript, materialNameToSearch);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Assign Material To Selected Indices", EditorStyles.boldLabel);

        indicesToUpdate = EditorGUILayout.TextField("Indices To Update (e.g., 0,2,3)", indicesToUpdate);
        materialNameToAssign = EditorGUILayout.TextField("New Material Name", materialNameToAssign);

        if (GUILayout.Button("Assign Material To Selected Indices"))
        {
            AssignMaterialToSelectedIndices(targetScript, indicesToUpdate, materialNameToAssign);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Assign Material To Variant by Index", EditorStyles.boldLabel);

        targetVariantIndex = EditorGUILayout.IntField("Variant Index to Update", targetVariantIndex);
        variantMaterialName = EditorGUILayout.TextField("New Material Name", variantMaterialName);

        if (GUILayout.Button("Assign Material To All Entries in Variant"))
        {
            AssignMaterialToWholeVariant(targetScript, targetVariantIndex, variantMaterialName);
        }
    }

    private void PopulateMatchingRenderers(AnimatedObjectsOfModules moduleScript, string materialName)
    {
        if (string.IsNullOrEmpty(materialName)) return;

        MeshRenderer[] allMeshRenderers = GameObject.FindObjectsOfType<MeshRenderer>();

        foreach (var variant in moduleScript.materialSettings)
        {
            // Clear existing material settings
            variant.materialSettings.Clear();

            foreach (MeshRenderer mr in allMeshRenderers)
            {
                foreach (var mat in mr.sharedMaterials)
                {
                    if (mat != null && mat.name == materialName)
                    {
                        var newSetting = new MaterialSettings
                        {
                            meshrenderObject = mr.gameObject,
                            materialsToChange = new Material[] { mat }
                            // currentMaterials is intentionally not set
                        };

                        variant.materialSettings.Add(newSetting);
                        Debug.Log($"✔️ Added '{mr.gameObject.name}' to variant '{variant.variantName}'");
                        break;
                    }
                }
            }
        }

        EditorUtility.SetDirty(moduleScript);
        Debug.Log("✅ Finished populating mesh renderers.");
    }

    private void AssignMaterialToSelectedIndices(AnimatedObjectsOfModules moduleScript, string indexList, string materialName)
    {
        if (string.IsNullOrEmpty(indexList) || string.IsNullOrEmpty(materialName))
        {
            Debug.LogWarning("Material name or indices list is empty.");
            return;
        }

        Material targetMaterial = FindMaterialByName(materialName);
        if (targetMaterial == null)
        {
            Debug.LogError($"❌ Material '{materialName}' not found in project.");
            return;
        }

        string[] indexStrings = indexList.Split(',');
        HashSet<int> indices = new HashSet<int>();

        foreach (var str in indexStrings)
        {
            if (int.TryParse(str.Trim(), out int index))
                indices.Add(index);
        }

        foreach (var variant in moduleScript.materialSettings)
        {
            for (int i = 0; i < variant.materialSettings.Count; i++)
            {
                if (indices.Contains(i))
                {
                    variant.materialSettings[i].materialsToChange = new Material[] { targetMaterial };
                    Debug.Log($"✅ Assigned '{materialName}' to index {i} in variant '{variant.variantName}'");
                }
            }
        }

        EditorUtility.SetDirty(moduleScript);
    }

    private void AssignMaterialToWholeVariant(AnimatedObjectsOfModules moduleScript, int variantIndex, string materialName)
    {
        if (variantIndex < 0 || variantIndex >= moduleScript.materialSettings.Count)
        {
            Debug.LogError("❌ Invalid variant index.");
            return;
        }

        Material targetMaterial = FindMaterialByName(materialName);
        if (targetMaterial == null)
        {
            Debug.LogError($"❌ Material '{materialName}' not found.");
            return;
        }

        VariantData variant = moduleScript.materialSettings[variantIndex];
        foreach (var setting in variant.materialSettings)
        {
            setting.materialsToChange = new Material[] { targetMaterial };
        }

        Debug.Log($"✅ Assigned '{materialName}' to all entries in variant '{variant.variantName}'");
        EditorUtility.SetDirty(moduleScript);
    }

    private Material FindMaterialByName(string materialName)
    {
        string[] guids = AssetDatabase.FindAssets($"t:Material {materialName}");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat != null && mat.name == materialName)
                return mat;
        }
        return null;
    }
}
#endif
