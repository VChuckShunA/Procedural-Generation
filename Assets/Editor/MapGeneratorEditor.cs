using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        //casting target to mapgenerator
        MapGenerator mapGen = (MapGenerator)target;

        if (DrawDefaultInspector()) {
            if (mapGen.autoUpdate) {
                mapGen.GenerateMap();
            }
        }

        DrawDefaultInspector();

        if (GUILayout.Button("Generate")){
            mapGen.GenerateMap();
        }
    }
}
