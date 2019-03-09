using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddMaterialsToSelf : MonoBehaviour
{
    //Script gets executed on button press of the AddMaterialsEditor script
    [Header("Material")]
    public Material material;
    public void AddMaterialToSelectedGameObjects()
    {
        {
            GameObject[] gameObjects = Selection.gameObjects;
            foreach (GameObject go in gameObjects)
            {
                #region Materials
                MeshRenderer MeshRenderer = go.GetComponent<MeshRenderer>();
                if (material)
                {
                    MeshRenderer.material = material;
                }
                #endregion
            }
        }
    }
}