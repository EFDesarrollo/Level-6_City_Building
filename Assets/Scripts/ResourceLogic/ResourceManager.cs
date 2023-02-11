using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public ResourceType resourceType; // variable indicating the type of resource (wood, gold, metal, copper, stone)
    public int fixedAmount; // variable containing a fixed amount of units
    public bool unlimitedAmount; // variable that indicates if you have an unlimited amount or not
    public int maxUnits;
    private int currentUnits;

    // Method that allows subtracting a number of units from the amount, as long as it is not unlimited
    public void SubtractAmount(int amount)
    {
        if (!unlimitedAmount)
        {
            fixedAmount -= amount;
            if (fixedAmount <= 0)
            {
                Destroy(gameObject); // removes the gameObject from the scene when units are depleted
            }
        }
    }
    public int ExtractUnits(int amount)
    {
        // Don't extract more units than the current amount
        int extractedAmount = Mathf.Min(amount, currentUnits);

        // Subtract the extracted units from the current amount
        currentUnits -= extractedAmount;

        // If there are no more units, destroy the game object
        if (currentUnits <= 0)
        {
            Destroy(gameObject);
        }

        return extractedAmount;
    }

    
}
