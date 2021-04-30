using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentMission : MonoBehaviour
{

    public TextMeshProUGUI missionText;

    public Sprite emptyStar;
    public Sprite fullStar;

    public Image star1;
    public Image star2;

    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        missionText.text = MissionManager.instance.currentMission.missionText;
        

        if (PlaneManager.instance._planeIntegrity.currentIntegrity < 50) EmptyStar(star2);
        else FullStar(star2);

        int totalAverage = 0;

        foreach (ShippingPackage sp in MissionManager.instance.currentMission.packages)
        {
            if (sp.quantity > 0) return;

            totalAverage += CargoManager.instance.GetAverage(sp.itemName);
        }


        if (totalAverage / MissionManager.instance.currentMission.packages.Count < 50) EmptyStar(star1);
        else FullStar(star1);
    }

    public void EmptyStar(Image star)
    {
        star.sprite = emptyStar;
    }

    public void FullStar(Image star)
    {
        star.sprite = fullStar;
    }
}
