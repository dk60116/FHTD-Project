using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapBuilder))]
public class MapBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapBuilder _cMap = target as MapBuilder;

        if (GUILayout.Button("Generate Map"))
        {
            _cMap.GenerateMap();
            _cMap.GenerateMap();
        }
    }
}
