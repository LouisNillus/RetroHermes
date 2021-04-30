using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanel : MonoBehaviour
{

    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y) && PlaneManager.instance._planeLanding.currentCity == false)
        {
            PlaneManager.instance.landed = true;
            PlaneManager.instance._planeMovement.KillSpeed();
            PanelState(true);
        }

        if (Input.GetKeyDown(KeyCode.U) && PlaneManager.instance._planeLanding.currentCity == false)
        {
            PlaneManager.instance.TakeOff();
            PanelState(false);
        }
    }

    public void PanelState(bool state)
    {
        panel.SetActive(state);
    }
}
