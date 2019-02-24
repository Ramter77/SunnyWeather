using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePrefab : MonoBehaviour
{
    [Header ("Prefabs")]
    [Tooltip ("Prefabs to place on Terrain with assigned HotKeys (Place Prefab on IgnoreRaycast Layer for now)")]
    [SerializeField]
    private GameObject Prefab;
    private GameObject currentPrefab;

    [Header ("Controls")]
    [Tooltip("HotKeys to place assigned Prefabs")]
    [SerializeField]
    private KeyCode hotkey = KeyCode.Mouse0;
    [Tooltip ("Check to Instantiate Prefabs straight")]
    [SerializeField]
    private Boolean fixedAngle;
    private float mouseWheelRotation;
    [SerializeField]
    private float mouseWheelRotationMultiplier = 0.1f;

    private void Update()
    {
        checkHotKeys(); //Instantiates Prefab & sets it to currentPrefab to use for following functions

        if (currentPrefab != null) {
            MovePrefabToViewPort();
            RotatePrefabByScrolling();
            PlacePrefabOnRelease();
        }
    }

    private void checkHotKeys() {
        //If hotKey is pressed Instantiate Prefab & assign as currentPrefab
        if (Input.GetKeyDown(hotkey)) {
            if (currentPrefab == null) {
                currentPrefab = Instantiate(Prefab);
            }
        }
    }

    private void MovePrefabToViewPort() {      //! Place Prefab on IgnoreRaycast Layer for now
        //Cast a ray to the middle of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            //Move currentPrefab to rayCastHit position + half of its scale (//TODO: later create a variable if needed)
            float halfScale = currentPrefab.transform.localScale.y/2f;
            currentPrefab.transform.position = new Vector3(hit.point.x, hit.point.y + halfScale, hit.point.z);

            //Make it stand on hit Terrain with 90 degree angle
            if (!fixedAngle) {
                currentPrefab.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }

    private void RotatePrefabByScrolling() {
        //Rotate the currentPrefab by scrolling the mouseWheel
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPrefab.transform.Rotate(Vector3.up, mouseWheelRotation * mouseWheelRotationMultiplier);
    }

    private void PlacePrefabOnRelease()
    {
        //If hotKey is released then currentObject & mouseWheelRotation are reset
        if (Input.GetKeyUp(hotkey)) {
            currentPrefab = null;
            mouseWheelRotation = 0;
        }
    }
}
