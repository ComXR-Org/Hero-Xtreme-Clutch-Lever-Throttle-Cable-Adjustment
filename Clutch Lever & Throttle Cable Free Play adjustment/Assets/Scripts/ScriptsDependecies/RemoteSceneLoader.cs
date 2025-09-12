using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RemoteSceneLoader : MonoBehaviour
{ 
  
    public AssetReference scene;
 
    [SerializeField]private GameObject RnR;


    private void Awake()
    {
        AsyncOperationHandle handle = scene.LoadAssetAsync<GameObject>();
        handle.Completed += Handle_Completed;


    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Handle_Completed(AsyncOperationHandle obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
          Instantiate(scene.Asset, transform);
            RnR = GameObject.Find("HeroXtream160r_RearWheel(Clone)");
        }
        else
        {
            Debug.LogError("AssetReference failed to load.");
        }
    }
    void activeGameObject()
    {

        RnR.SetActive(true);

    }
    void DectiveGameObject()
    {
        RnR.SetActive(false);
    }
    private void Update()
    {

    }
    private void RemoteSceneLoader_Completed(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
           
            RnR = Instantiate(obj.Result);
            RnR.SetActive(false);
            Debug.Log("Loaded Scene");
        }
       
    }
}
