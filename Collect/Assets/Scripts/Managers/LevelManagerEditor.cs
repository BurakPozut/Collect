using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        LevelManager script = (LevelManager) target;
        if(GUILayout.Button("Save Data"))   script.SaveFromEditor();
        if(GUILayout.Button("Get LevelIndex "))   script.GetLEvelIndex();
        
    }
}

