using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class City : MonoBehaviour
{
    public static City instance;
    [Header("Inventory of resources in city")]
    public ResourceInventory[] resourcesInventory;
    public Dictionary<ResourceType, ResourceInventory> resourceInventoryDictionary;

    [Header("List of created buildings in world")]
    public List<Building> buildings = new List<Building>();


    private void Awake()
    {
        instance = this;
        resourceInventoryDictionary = resourcesInventory.ToDictionary(x => x.ResourceType);
    }

    private void Start()
    {
        /*UpdateStatText();*/
    }

    public bool CheckEnougthResources(BuildingPreset building)
    {
        ResourceInventory resourceInventory;
        bool checks = true;

        foreach (ResourceCost resourceCost in building.buildingCosts)
            if (resourceInventoryDictionary.TryGetValue(resourceCost.ResourceType, out resourceInventory))
                checks = checks && resourceInventory.CurrentUnits >= resourceCost.CostUnits;
        return checks;
    }

    //called when we place down a building
    public void OnPlaceBuilding (Building building)
    {
        ResourceInventory resourceInventory;
        
        foreach (ResourceCost resourceCost in building.preset.buildingCosts)
            if (resourceInventoryDictionary.TryGetValue(resourceCost.ResourceType, out resourceInventory))
                UpdateResourceInventory(resourceInventory, -resourceCost.CostUnits);
        
        foreach (AddResource addResource in building.preset.addResources)
            if (resourceInventoryDictionary.TryGetValue(addResource.ResourceType, out resourceInventory))
                UpdateResourceInventory(resourceInventory, addResource.Units);

        buildings.Add(building);
    }
    public void UpdateResourceInventory(ResourceInventory resourceInventory, int units)
    {
        resourceInventory.CurrentUnits += units;
        if (resourceInventory.CurrentUnits > resourceInventory.MaxUnits)
            resourceInventory.CurrentUnits = resourceInventory.MaxUnits;
        if (resourceInventory.CurrentUnits < 0)
            resourceInventory.CurrentUnits = 0;
        UIDisplayManager.Instance.UpdateDisplay();
    }
    public void UpdateResourceMaxInventory(ResourceInventory resourceInventory, int units)
    {
        resourceInventory.MaxUnits += units;
        if (resourceInventory.MaxUnits < 0)
            resourceInventory.MaxUnits = 0;
        UIDisplayManager.Instance.UpdateDisplay();
    }

    //called when we bulldoze a building
    public void OnRemoveBuilding(Building building)
    {
        buildings.Remove(building);
        Destroy(building.gameObject);
    }
}
