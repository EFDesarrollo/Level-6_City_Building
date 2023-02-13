using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public ResourceType resourceType; // variable indicating the type of resource (wood, gold, metal, copper, stone)
    public bool unlimitedAmount; // variable that indicates if you have an unlimited amount or not
    public int maxUnits; // variable containing a fixed amount of units
    public int currentUnits;
    private void Start()
    {
        currentUnits = maxUnits;
    }
    public int ExtractUnits(int amount)
    {
        // Don't extract more units than the current amount
        int extractedAmount = Mathf.Min(amount, currentUnits);

        // Subtract the extracted units from the current amount if aren't unlimited
        if (!unlimitedAmount)
            currentUnits -= extractedAmount;

        // If there are no more units, destroy the game object
        if (currentUnits <= 0)
        {
            Destroy(gameObject);
        }

        return extractedAmount;
    }

    
}
