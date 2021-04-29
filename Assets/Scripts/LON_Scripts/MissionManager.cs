using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{

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
        
    }

    public void LoadMission(ShippingMission sm)
    {
        currentMission = sm;
    }


    public void CheckShipping(ItemType soldItem)
    {
        if(currentMission.destinationID == Shop.instance.currentIsland.ID && currentMission.complete == false)
        for (int i = 0; i < currentMission.packages.Count; i++)
        {
            ShippingPackage sp = currentMission.packages[i];
            if (soldItem == sp.itemName) sp.quantity--;
        }

        MissionCompletion();
    }

    public void MissionCompletion()
    {
        foreach(ShippingPackage sp in currentMission.packages)
        {
            if (sp.quantity > 0) return;
        }

        Inventory.instance.Earn(currentMission.reward);

        missionIndex++;
        missionIndex = Mathf.Clamp(missionIndex, 0, missions.Count - 1);

        currentMission = missions[missionIndex];
    }
}

[System.Serializable]
public class ShippingMission
{
    public List<ShippingPackage> packages = new List<ShippingPackage>();
    [Range(0,4)]
    public int destinationID;
    [Range(0,2000)]
    public int reward;
    public bool complete = false;

}

[System.Serializable]
public class ShippingPackage
{
    public ItemType itemName;
    [Range(0, 20)]
    public int quantity;
}