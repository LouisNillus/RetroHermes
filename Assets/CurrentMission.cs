using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentMission : MonoBehaviour
{

    public TextMeshProUGUI missionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        missionText.text = MissionManager.instance.currentMission.missionText;
    }
}
