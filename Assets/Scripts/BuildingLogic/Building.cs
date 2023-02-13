using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
        public BuildingPreset preset;

    private void OnDestroy()
    {
        ResourceInventory resourceInventory;
        foreach (ResourceCost resourceCost in preset.buildingCosts)
        {
            if (resourceCost.ResourceType == ResourceType.Population)
            {
                City.instance.resourceInventoryDictionary.TryGetValue(ResourceType.Population, out resourceInventory);
                City.instance.UpdateResourceInventory(resourceInventory, resourceCost.CostUnits);
            }
        }
        City.instance.resourceInventoryDictionary.TryGetValue(ResourceType.Gold, out resourceInventory);
        City.instance.UpdateResourceInventory(resourceInventory, 5);
    }
}
