using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AddCollidersnRBsToSelectedGameObjectsScript))]
[CanEditMultipleObjects]
public class AddCollidersnRBsToSelectedGameObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        AddCollidersnRBsToSelectedGameObjectsScript script = (AddCollidersnRBsToSelectedGameObjectsScript)target;

        if(GUILayout.Button("Add Material to all selected GameObjects"))
        {
            script.AddMaterialToSelectedGameObjects();
        }

        if(GUILayout.Button("Add Colliders to all selected GameObjects"))
        {
            script.AddCollidersToSelectedGameObjects();
        }

        if(GUILayout.Button("Add RigidBodies to all selected GameObjects"))
        {
            script.AddRigidBodiesToSelectedGameObjects();
        }
    }
}
