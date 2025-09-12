using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAssetBundles : MonoBehaviour {

    [MenuItem("VW/Build Asset Bundles")]
    static void BuildABs() {
        string dir = Application.dataPath+Path.DirectorySeparatorChar+"AssetBundles";
        dir = dir.Replace("/Assets", "");
        if (!Directory.Exists(dir)) {
            Debug.Log(dir);
            Directory.CreateDirectory(dir);
        }
        // Put the bundles in a folder called "ABs" within the Assets folder.
        BuildPipeline.BuildAssetBundles("AssetBundles/", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
