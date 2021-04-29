using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatchMouseClicks : MonoBehaviour
{

    GameObject lastSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != null)
        lastSelected = EventSystem.current.currentSelectedGameObject;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            CatchClicks();
        }

    }


    public void CatchClicks()
    {
        EventSystem.current.SetSelectedGameObject(lastSelected);
    }
}
