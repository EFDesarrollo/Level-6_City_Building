using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Wood,
    Gold,
    Iron,
    Copper,
    Stone,
    Food,
    Population
}

[System.Serializable]
public class ResourceInventory
{
    public Sprite Sprite;
    public ResourceType ResourceType;
    public int CurrentUnits;
    public int MaxUnits;
}

[System.Serializable]
public class ResourceCost
{
    public ResourceType ResourceType;
    public int CostUnits;
    public int CostPerTurn;
}

[System.Serializable]
public class AddResource
{
    public ResourceType ResourceType;
    public int Units;
}
