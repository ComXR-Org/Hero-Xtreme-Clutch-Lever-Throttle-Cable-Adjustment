using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialAccess {
    
    public Material[] MatArray;
}

public class SwitchMatOnChildren : MonoBehaviour
{
    public MaterialAccess[] OgMat;
    public List<Material> oldMat;
    public Material NewMat;
    public MeshRenderer[] meshRenderers;
    public bool IsMatChanged = false;
    [SerializeField]
    private bool prevMat = false;
    //private void Awake() {
    //    meshRenderers = GetComponentsInChildren<MeshRenderer>();
    //    OgMat = new MaterialAccess[meshRenderers.Length];
    //    Debug.Log("Ogmatlength" + OgMat.Length);
    //    for (int i = 0; i < meshRenderers.Length; i++) {
    //        Debug.Log("Mats" + meshRenderers[i].materials.Length);
    //        Debug.Log("Mats" + OgMat[i]);
    //        OgMat[i].MatArray = new Material[1];
    //        OgMat[i].MatArray = meshRenderers[i].materials;
    //        Debug.Log("Mats" + OgMat[i].MatArray.Length);

    //    }
    //}

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer r in meshRenderers)
        {
            Debug.Log("this material: " + r.materials);
            oldMat.Add(r.material);
            if(r.materials.Length > 1)
            {
                //foreach(Material m in r.materials)
                //{
                //    oldMat.Add(r.material);
                //}

                for (int i = 1; i < r.materials.Length; i++)
                {
                   oldMat.Add(r.materials[i]);
                }
            }
        }
        /*
        //meshRenderers = GetComponentsInChildren<MeshRenderer>();
        OgMat = new MaterialAccess[meshRenderers.Length];
        Debug.Log("Ogmatlength" + OgMat.Length);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            Debug.Log("Mats" + OgMat[i]);
            OgMat[i].MatArray = new Material[1];
            OgMat[i].MatArray = meshRenderers[i].materials;
            Debug.Log("Mats" + OgMat[i].MatArray.Length);

        //}
        */
    }
    //private void Update()     
    //{
    //    if (IsMatChanged)
    //    {
    //        for (int i=0; i<meshRenderers.Length;i++) 
    //        {

    //            meshRenderers[i].material = NewMat;
    //        }
    //    }
    //    else {
    //        if (meshRenderers.Length > 0&&OgMat.Length>0) {
    //            for (int i = 0; i < meshRenderers.Length; i++) {

    //                    meshRenderers[i].materials = OgMat[i].MatArray;


    //            }
    //        }
    //    }

    //}


    void Update()
    {        
        if (IsMatChanged)
        {
            foreach (MeshRenderer r in meshRenderers)
            {
                // Debug.Log("contain 1 mat" + r.material);

                r.material = NewMat;
                if (r.materials.Length > 1)
               {

                    Material[] mats = new Material[r.materials.Length];

                    for (int i=0; i< r.materials.Length; i++)
                    {
                        mats[i] = NewMat;    
                        
                    }
                    r.materials = mats;
                                       
               }
                //else
                //{
                //    r.material = NewMat;
                //}
            }
            IsMatChanged = false;

        }
        if(prevMat)
        {
            for (int i = 0; i < oldMat.Count -1; i++)
            {
                
                meshRenderers[i].material = oldMat[i];

                if (meshRenderers[i].materials.Length > 1)
                {

                    Material[] prevmats = new Material[meshRenderers[i].materials.Length];
                    for (int j = 0; j < meshRenderers[i].materials.Length +1; j++)
                    {
                        
                        prevmats[j] = oldMat[i];
                        Debug.Log(prevmats[j]);

                        i++;
                    }


                    meshRenderers[i-2].materials = prevmats;
                    
                }
            }
            
            prevMat = false;
        }
    }
}
