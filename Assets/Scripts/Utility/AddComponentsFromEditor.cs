using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AddMaterialsToSelf))]
[CanEditMultipleObjects]
public class AddComponentsFromEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AddMaterialsToSelf script = (AddMaterialsToSelf)target;

        if (GUILayout.Button("Add Material to all selected GameObjects"))
        {
            script.AddMaterialToSelectedGameObjects();
        }
    }
}