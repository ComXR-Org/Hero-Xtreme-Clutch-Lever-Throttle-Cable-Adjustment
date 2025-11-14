using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainBikeVariants : MonoBehaviour
{


    [System.Serializable]
    public class MaterialSettings
    {
        public GameObject meshrenderObject;
        public Material[] currentMaterials;
        public Material[] materialsToChange;
    }

    [System.Serializable]
    public class VariantData
    {
        public string variantName;
        public List<MaterialSettings> materialSettings = new List<MaterialSettings>();
        public GameObject sticker;
    }

    public List<GameObject> groupNames = new List<GameObject>(); // List of bike variants to choose from
    [Header("Material Changing Settings"), Space(5)]
    public VarientType currentVariantName;
    public List<VariantData> materialSettings = new List<VariantData>();
    void OnEnable()
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

    void OnDisable()
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
                    if (settings.meshrenderObject == null || settings.materialsToChange == null)
                        continue;

                    Renderer renderer = settings.meshrenderObject.GetComponent<Renderer>();
                    if (renderer == null) continue;

                    Material[] currentMats = renderer.materials;

                    // Single material → replace directly
                    if (currentMats.Length == 1)
                    {
                        renderer.material = settings.materialsToChange[0];
                    }
                    else
                    {
                        var mats = renderer.materials;
                        for (int i = 0; i < renderer.materials.Length; i++)
                        {
                            if (mats[i].name.Contains(settings.currentMaterials[0].name))
                            {
                                mats[i] = settings.materialsToChange[0];
                            }
                        }
                        renderer.materials = mats;
                    }
                }
            }
            else
            {
                variant.sticker.SetActive(false);
            }
        }
    }


    public void ResetMaterials()
    {
        foreach (VariantData variant in materialSettings)
        {
            variant.sticker.SetActive(false);
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
