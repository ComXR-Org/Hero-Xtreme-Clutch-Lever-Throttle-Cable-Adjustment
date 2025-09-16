using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeMatTexChange : MonoBehaviour
{
    [System.Serializable]
    public class BikeVariant
    {
        public string colorName;
        public materialwithVarient[] matsAndTex;
        public Color colorToApply;
        public Material[] materialsForColorChange;

    }

    [System.Serializable]
    public class materialwithVarient
    {
        public Material mat;
        public Texture tex;
    }
    public BikeVariant[] colorVariants;
    BikeVariant SelectedColorVariants;
    public void changeBikeColor(string colorName)
    {

        foreach (BikeVariant bikeVariant in colorVariants)
        {
            if (bikeVariant.colorName == colorName)
            {
                SelectedColorVariants = bikeVariant;
            }
        }

        for (int i = 0; i < SelectedColorVariants.matsAndTex.Length; i++)
        {
            SelectedColorVariants.matsAndTex[i].mat.mainTexture = SelectedColorVariants.matsAndTex[i].tex;
        }
        for (int i = 0; i < SelectedColorVariants.materialsForColorChange.Length; i++)
        {
            SelectedColorVariants.materialsForColorChange[i].color = SelectedColorVariants.colorToApply;
        }
    }
}
