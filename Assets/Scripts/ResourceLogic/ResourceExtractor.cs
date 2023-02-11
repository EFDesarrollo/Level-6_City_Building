using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Wood,
    Gold,
    Metal,
    Copper,
    Stone
}
public class ResourceExtractor : MonoBehaviour
{
    public ResourceType extractableResource;
    public float rangeOfDetection;
    public int extractionAmount;
    public float extractionRate;
    private float timer;
    private int extractedResourceAmount;

    private void Update()
    {
        // Check if it's time to extract resources
        timer += Time.deltaTime;
        if (timer >= extractionRate)
        {
            timer = 0;

            // Get the resources in the adjacent tiles
            Collider[] resources = Physics.OverlapSphere(transform.position, rangeOfDetection, LayerMask.GetMask("Resource"));
            foreach (Collider col in resources)
            {
                ResourceManager resource = col.GetComponent<ResourceManager>();
                // Check if the resource can be extracted
                if (resource.resourceType == extractableResource)
                {
                    extractedResourceAmount += resource.ExtractUnits(extractionAmount);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, rangeOfDetection);
    }
}
