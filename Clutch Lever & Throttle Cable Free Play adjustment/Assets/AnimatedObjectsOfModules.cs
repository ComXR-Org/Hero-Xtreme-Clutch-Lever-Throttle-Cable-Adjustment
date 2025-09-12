using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MaterialSettings
{    
    public GameObject meshrenderObject;
    public Material[] currentMaterials;
    public Material[] materialsToChange;
}

[System.Serializable]
public class VariantData {
    public string variantName;
    public List<MaterialSettings> materialSettings = new List<MaterialSettings>();
    public GameObject sticker;
}

public enum VarientType
{
    Yellow,
    Red,
    Black,
    Combat
}
public class AnimatedObjectsOfModules : MonoBehaviour
{

    public bool isWholeBikeUsed;
    public string[] partsToDisableInModule;


    [Header("Material Changing Settings"), Space(5)]
    public VarientType currentVariantName;
    public List<VariantData> materialSettings = new List<VariantData>();

    private void Start()
    {
        switch (currentVariantName)
        {
            case VarientType.Yellow:
                ChangeMaterials("Yellow");
                break;
            case VarientType.Red:
                ChangeMaterials("Red");
                break;
            case VarientType.Black:
                ChangeMaterials("Black");
                break;
            case VarientType.Combat:
                ChangeMaterials("Combat");                
                break;
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetMaterials();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeMaterials(currentVariantName.ToString());
        }
    }
    public void ChangeMaterials(string variantName)
    {
        foreach (VariantData variant in materialSettings)
        {
            if (variant.variantName == variantName)
            {
                variant.sticker.SetActive(true);
                foreach (MaterialSettings settings in variant.materialSettings)
                {
                    if (settings.meshrenderObject != null && settings.materialsToChange != null)
                    {
                        Renderer renderer = settings.meshrenderObject.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            // Store current materials before changing
                            settings.currentMaterials = renderer.materials;

                            // Apply new materials
                            renderer.materials = settings.materialsToChange;
                        }
                    }
                }
            }
            else
            {
                variant.sticker.SetActive(false);
            }
        }
    }

    private void OnApplicationQuit()
    {
        
    }

    public void ResetMaterials()
    {
        foreach (VariantData variant in materialSettings)
        {
            foreach (MaterialSettings settings in variant.materialSettings)
            {
                if (settings.meshrenderObject != null && settings.currentMaterials != null)
                {
                    Renderer renderer = settings.meshrenderObject.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.materials = settings.currentMaterials;
                    }
                }
            }
        }
    }
}
