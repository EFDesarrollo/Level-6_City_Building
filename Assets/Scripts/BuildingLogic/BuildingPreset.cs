using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Building Preset", menuName = "New Building Preset")]
public class BuildingPreset : ScriptableObject
{
    public string presetName;
    public Sprite Sprite;
    public GameObject prefab;
    public ResourceCost[] buildingCosts;
    public AddResource[] addResources;
}
