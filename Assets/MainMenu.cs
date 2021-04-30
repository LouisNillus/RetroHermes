using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlaneManager.instance.landed = true;
        PlaneManager.instance._planeMovement.KillSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
            this.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        PlaneManager.instance.TakeOff();
    }
}
