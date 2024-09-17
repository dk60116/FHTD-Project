using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class EssetBundleBuildEditor : Editor
{
    [MenuItem("Assets/Build Bundle")]
    static void BuildBundle()
    {
        foreach (var item in new DirectoryInfo(Application.dataPath + "/AssetBundles_PC").GetFiles())
            item.Delete();
        foreach (var item in new DirectoryInfo(Application.dataPath + "/AssetBundles_Android").GetFiles())
            item.Delete();
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles_PC", BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows);
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles_Android", BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.Android);
    }
}
