using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class MissionManager : MonoBehaviour
{

    public TextMeshProUGUI missionName;
    public TextMeshProUGUI missionText;
    public ShippingMission currentMission;

    public List<ShippingMission> missions = new List<ShippingMission>();

    public static MissionManager instance;
    int missionIndex = 0;

    private void Awake()
    {
        instance = this;
        currentMission = missions[missionIndex];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMission != null)
        {
            missionName.text = "#" + (missionIndex + 1) + " " + currentMission.missionName;
            missionText.text = currentMission.missionText;
        }
    }

    public void LoadMission(ShippingMission sm)
    {
        currentMission = sm;
    }


    public void CheckShipping(ItemType soldItem)
    {
        if(currentMission.destinationID == Shop.instance.currentIsland.ID && currentMission.complete == false)
        {
            for (int i = 0; i < currentMission.packages.Count; i++)
            {
                ShippingPackage sp = currentMission.packages[i];
                if (soldItem == sp.itemName) sp.quantity--;
            }

            MissionCompletion();
        }
    }

    public void MissionCompletion()
    {
        int totalAverage = 0;

        foreach(ShippingPackage sp in currentMission.packages)
        {
            if (sp.quantity > 0) return;

            totalAverage += CargoManager.instance.GetAverage(sp.itemName);
        }

        if (PlaneManager.instance._planeIntegrity.currentIntegrity > 50) currentMission.stars++;
        if (totalAverage / currentMission.packages.Count > 50) currentMission.stars++;

        Inventory.instance.Earn(currentMission.rewardPerStar * (currentMission.stars + 1));

        currentMission.complete = true;

        missionIndex++;
        missionIndex = Mathf.Clamp(missionIndex, 0, missions.Count - 1);
        
        currentMission = missions[missionIndex];
    }
}

[System.Serializable]
public class ShippingMission
{
    [Header("Mission")]
    public string missionName;
    [TextArea(3,4)]
    public string missionText;
    [TextArea(5, 10)]
    public string missionPath;
    [Range(0,4)]
    public int destinationID;
    [Range(0,2000)]
    public int rewardPerStar;
    [ReadOnly] public int stars;
    [ReadOnly] public bool complete = false;
    public List<ShippingPackage> packages = new List<ShippingPackage>();

}

[System.Serializable]
public class ShippingPackage
{
    public ItemType itemName;
    [Range(0, 20)]
    public int quantity;
}