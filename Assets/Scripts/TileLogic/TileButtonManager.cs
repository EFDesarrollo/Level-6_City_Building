using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButtonManager : MonoBehaviour
{
    // Transform component of the tile
    private Transform tileTransform;
    // Direction in which the new tile should be placed
    private Vector3 direction;

    /// <summary>
    /// Function to handle the click event of the button
    /// </summary>
    public void OnClickAddTile()
    {
        // Call the AddTerrainTile method of the ProceduralTerrainGeneratorManager instance with the required parameters
        ProceduralTerrainGeneratorManager.instance.AddTerrainTile(tileTransform.position, direction, tileTransform.GetComponent<TileManager>().biome);
    }

    /// <summary>
    /// Function to set the values of the tileTransform and direction variables
    /// </summary>
    /// <param name="tileT"></param>
    /// <param name="tileDirection"></param>
    public void SetButtonValues(Transform tileT, Vector3 tileDirection)
    {
        // Assign the given tileTransform to the tileTransform variable
        tileTransform = tileT;
        // Assign the given direction to the direction variable
        direction = tileDirection;
    }
}
