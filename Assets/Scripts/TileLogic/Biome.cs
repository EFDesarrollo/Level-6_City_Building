using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Biome class that has four properties: probability, tilePrefab, resourcesPool and resourcesRange.
/// the probability property is the probability of appearance of the biome in percentage
/// (for example, if probability is 25, it means that the biome will appear in 25% of the cases),
/// The tilePrefab property is a game object that represents the biome,
/// resourcesPool stores the resources that can be instantiated within a biome,
/// and resourcesRange contains the maximum and minimum values ??of resources that the biome can contain.
/// </summary>
[System.Serializable]
public class Biome
{
    /// <summary>
    /// probability of appearance of the biome in percentage
    /// </summary>
    public int probability;
    /// <summary>
    /// game object representing the biome
    /// </summary>
    public GameObject tilePrefab;
    /// <summary>
    /// stores the resources that can be instantiated within
    /// </summary>
    public List<GameObject> resourcesPool;
    /// <summary>
    /// contains the maximum and minimum values ??of resources that the biome can contain.
    /// </summary>
    public ResourcesRange resourcesRange;
}

[System.Serializable]
public class ResourcesRange
{
    public int maxResources, minResources;
}

//[CustomPropertyDrawer(typeof(ResourcesRange))]
//public class TwoVariablesDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        float originalLabelWidth = EditorGUIUtility.labelWidth;
//        EditorGUIUtility.labelWidth = 50;

//        Rect var1Rect = new Rect(position.x, position.y, position.width / 2 - 5, position.height);
//        Rect var2Rect = new Rect(position.x + position.width / 2, position.y, position.width / 2 - 5, position.height);

//        EditorGUI.PropertyField(var1Rect, property.FindPropertyRelative("maxResources"), new GUIContent("Max R"));
//        EditorGUI.PropertyField(var2Rect, property.FindPropertyRelative("minResources"), new GUIContent("Min R"));

//        EditorGUIUtility.labelWidth = originalLabelWidth;
//    }
//}
