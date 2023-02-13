using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementMaxResources : MonoBehaviour
{
    public AddResource[] incrementMaxResources;
    // Start is called before the first frame update
    void Awake()
    {
        City city = City.instance;
        foreach (AddResource resource in incrementMaxResources)
        {
            city.resourceInventoryDictionary.TryGetValue(resource.ResourceType, out var resourceInventory);
            city.UpdateResourceMaxInventory(resourceInventory, resource.Units);
        }
    }

    private void OnDestroy()
    {
        City city = City.instance;
        foreach (AddResource resource in incrementMaxResources)
        {
            city.resourceInventoryDictionary.TryGetValue(resource.ResourceType, out var resourceInventory);
            city.UpdateResourceMaxInventory(resourceInventory, -resource.Units);
        }
    }
}
