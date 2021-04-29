using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    private bool mapActive = false;
    [SerializeField] private GameObject playerMap;

    private void Awake() => playerMap.SetActive(mapActive);

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) OpenMap();
    }

    void OpenMap()
    {
        playerMap.SetActive(PlaneManager.instance.openedMap = mapActive = !mapActive);
        /*if (mapActive) PlaneManager.instance._planeMovement.KillSpeed();
        else PlaneManager.instance._planeMovement.ResetSpeed();*/
    }
}