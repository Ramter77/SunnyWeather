using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefab : MonoBehaviour
{
    #region Variables
    //[Header("Prefabs")]
    [Tooltip("Prefabs to place with number keys 1-9 on colliders with assigned HotKey")]
    [SerializeField]
    private GameObject[] Prefabs;

    [Tooltip("Layer of colliders to place Prefabs on ('Ground')")]
    [SerializeField]
    private LayerMask mask;

    //Current selected prefab with assigned HotKey
    private GameObject currentPrefab;
    private int currentPrefabIndex = -1;


    [Header("Controls")]
    [Tooltip("HotKey to place assigned Prefabs")]
    [SerializeField]
    private KeyCode hotkey = KeyCode.Mouse0;

    [Tooltip("Maximum distance to place Prefabs")]
    [SerializeField]
    private float maxRayDistance = 1000;
    
    [Tooltip("Check to Instantiate Prefabs straight")]
    [SerializeField]
    private Boolean fixedAngle;

    [Tooltip("Check to place Prefabs in the middle of the screen")]
    [SerializeField]
    private Boolean fixedCameraPlacement;

    [Tooltip("Check to rotate Prefabs by a fixed value")]
    [SerializeField]
    private Boolean fixedRotation = true;

    private float mouseWheelRotation;
    [SerializeField]
    private float mouseWheelRotationMultiplier = 0.1f;
    #endregion

    private void Update()
    {
        checkHotKeys(); //Instantiates Prefab & sets it to currentPrefab to use for following functions
        if (currentPrefab != null)
        {
            MovePrefabToRayHit();
            RotatePrefabByScrolling();
            PlacePrefabOnRelease();
        }
    }

    #region checkHotKeys
    private void checkHotKeys()
    {
        for (int i = 0; i < Prefabs.Length; i++)
        {
            //If pressed a number key between 1-9
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                //If pressed again: reset
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(currentPrefab);
                    currentPrefabIndex = -1;
                }
                else
                {
                    if (currentPrefab != null)
                    {
                        Destroy(currentPrefab);
                    }

                    currentPrefab = Instantiate(Prefabs[i]);
                    currentPrefabIndex = i;
                }

                break;
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPrefab != null && currentPrefabIndex == i;
    }
    #endregion

    #region MovePrefabToRayHit
    private void MovePrefabToRayHit()
    {   //! Place Prefab on "Ground" layer
        Ray ray;
        RaycastHit hit;

        if (fixedCameraPlacement)
        {
            //Cast a ray to the middle of the screen
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        }
        else
        {
            //Cast a ray to the mouse position
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out hit, maxRayDistance, mask))
        {
            //Move currentPrefab to rayCastHit position + half of its scale (//TODO: later create a variable if needed)
            float halfScale = currentPrefab.transform.localScale.y / 2f;
            currentPrefab.transform.position = new Vector3(hit.point.x, hit.point.y + halfScale, hit.point.z);

            //Make it stand on hit Terrain with 90 degree angle
            if (!fixedAngle)
            {
                currentPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }
    #endregion

    #region RotatePrefabByScrolling
    private void RotatePrefabByScrolling()
    {
        if (fixedRotation) {
            //Rotate the currentPrefab by scrolling the mouseWheel
            mouseWheelRotation = Input.mouseScrollDelta.y;
            currentPrefab.transform.Rotate(Vector3.up, mouseWheelRotation * mouseWheelRotationMultiplier * 10);

            //Reset rotation
            mouseWheelRotation = 0;
        }
        else {
            mouseWheelRotation += Input.mouseScrollDelta.y;
            currentPrefab.transform.Rotate(Vector3.up, mouseWheelRotation * mouseWheelRotationMultiplier);
        }
        
    }
   #endregion

    #region PlaceOnRelease
    private void PlacePrefabOnRelease()
    {
        //If hotKey is pressed again then reset currentObject
        if (Input.GetKeyDown(hotkey))
        {
            currentPrefab = null;
        }
    }
    #endregion
}
