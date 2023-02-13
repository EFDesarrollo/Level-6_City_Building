using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceExtractor : MonoBehaviour
{
    public List<ResourceType> extractableResources;
    public float rangeOfDetection;
    public int extractionAmount;
    public float extractionRate;
    private float timer;
    private int extractedResourceAmount;

    static List<Vector3> NearSides = new List<Vector3> { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    private void Update()
    {
        // Check if it's time to extract resources
        timer += Time.deltaTime;
        if (timer >= extractionRate)
        {
            timer = 0;

            // Get the resources in the adjacent tiles
            Collider[] resourcesCol = GetVoxelSideColliders(transform.position, rangeOfDetection, LayerMask.GetMask("Resource"));
            foreach (Collider col in resourcesCol)
            {
                ResourceManager resource = col.GetComponent<ResourceManager>();
                // Check if the resource can be extracted
                if (extractableResources.Contains(resource.resourceType))
                    if (City.instance.resourceInventoryDictionary.TryGetValue(resource.resourceType, out var resourceInventory))
                        if (resourceInventory.CurrentUnits != resourceInventory.MaxUnits)
                            City.instance.UpdateResourceInventory(resourceInventory, resource.ExtractUnits(extractionAmount));
                    /*extractedResourceAmount += resource.ExtractUnits(extractionAmount);*/
            }
        }
    }

    private Collider[] GetVoxelSideColliders(Vector3 position, float range, int layerMask)
    {
        RaycastHit hit;
        List<Collider> cols = new List<Collider>();
        // for each side get behind collider
        for (int i = 0; i < NearSides.Count; i++)
        {
            if (Physics.Raycast(position, NearSides[i], out hit, range, layerMask))
                cols.Add(hit.collider);
        }

        return cols.ToArray();
    }

    private void OnDrawGizmos()
    {
        // Set the color of the gizmos to blue
        Gizmos.color = Color.blue;
        // Loop through all the NearSides
        for (int i = 0; i < NearSides.Count; i++)
        {
            // Calculate the size of the box based on the range of detection and the direction of the side
            Vector3 boxSize = new Vector3(1, 1, 1) + new Vector3(Mathf.Abs(NearSides[i].x), Mathf.Abs(NearSides[i].y), Mathf.Abs(NearSides[i].z)) * (rangeOfDetection - 1);
            // Draw a wireframe cube at the position of the side with the calculated size
            Gizmos.DrawWireCube(transform.position + NearSides[i] * (0.5f +rangeOfDetection/2), boxSize);
        }
    }
}