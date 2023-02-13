using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static ProceduralTerrainGeneratorManager;


public class ProceduralTerrainGeneratorManager : MonoBehaviour
{
    /// <summary>
    /// Declares a static instance of the class, which can be accessed from other scripts.
    /// </summary>
    static public ProceduralTerrainGeneratorManager instance;
    /// <summary>
    /// The size of each of the tiles that will be generated.
    /// </summary>
    public int TileSize = 1;
    /// <summary>
    /// The position on the y axis at which the tiles will align.
    /// </summary>
    public int vertical_alignment_position = -1;
    /// <summary>
    /// Contains the different biomes and their probabilities.
    /// </summary>
    public List<Biome> biomes;
    /// <summary>
    /// The probability of continue the Biome Tile;
    /// </summary>
    [Range(0.0f, 1.0f)]
    public float continue_Biome_Probability;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Assigns the instance variable to this object.
        instance = this;
    }
    private void Start()
    {
        AddTerrainTile(new Vector3(0, vertical_alignment_position, 0), new Vector3(0, 0, 0), biomes[0]);
    }
    /// <summary>
    /// Adds a new piece of terrain at a specific position and direction.
    /// The new position is calculated based on the specified base position and direction, and then the 
    /// Instantiate function is used to create a new piece of terrain at the calculated position. 
    /// The terrain tile is randomly selected from the list of biomes.
    /// </summary>
    /// <param name="curPos">The base position from which the new terrain tile will be added</param>
    /// <param name="direcction">The direction in which the new terrain tile will be added</param>
    public void AddTerrainTile(Vector3 curPos, Vector3 direcction, Biome biomeParent)
    {
        // Calculates the position of the new piece of terrain
        Vector3 newTilePos = curPos + direcction * TileSize;
        float randomValueToContinue = Random.Range(0.0f, 1.0f);

        if (randomValueToContinue <= 0.7f)
        {
            GameObject tile = Instantiate(biomeParent.tilePrefab, newTilePos, Quaternion.identity);
            tile.GetComponent<TileManager>().biome = biomeParent;
            InstantiateTileResources(newTilePos, biomeParent);
        }
        else
        {
            // event does not occur
            // Generates a random value between 0 and 100.
            int randomPercentValue = Random.Range(0, 100);
            // A cumulative percentage of the biomes' probabilities.
            int cumulativePercentage = 0;

            // Loops through the biomes and adds their probabilities to the cumulative percentage.
            foreach (Biome biome in biomes)
            {
                cumulativePercentage += biome.probability;
                // If the cumulative percentage is greater than the random value, instantiates the tile and the objects for this biome.
                if (randomPercentValue <= cumulativePercentage)
                {
                    // Instantiates the tile for the biome.
                    GameObject tile = Instantiate(biome.tilePrefab, newTilePos, Quaternion.identity);
                    tile.GetComponent<TileManager>().biome = biome;
                    // Instantiates the Resources for the biome.
                    InstantiateTileResources(newTilePos, biome);
                    break;
                }
            }
        }
    }
    /// <summary>
    /// Instantiates objects within a tile based on the specified biome.
    /// </summary>
    /// <param name="spawnCenter">The center position at which the objects will be instantiated</param>
    /// <param name="biome">The biome from which the objects will be instantiated</param>
    void InstantiateTileResources(Vector3 spawnCenter, Biome biome)
    {
        // calculates how many objects will be instantiated using the maximum and minimum of the biome.
        int numOfObjects = Random.Range(biome.resourcesRange.minResources, biome.resourcesRange.maxResources + 1);
        // Loops through the number of objects to be instantiated.
        for (int index = 0; index < numOfObjects; index++)
        {
            // Calculates a random position within the TileSize limits.
            Vector3 randomPos = new Vector3(Random.Range(-TileSize / 2, TileSize / 2), vertical_alignment_position, Random.Range(-TileSize / 2, TileSize / 2)) + Vector3.up;
            // Check if there is no object in the designated position
            if (!Physics.CheckSphere(randomPos, 0.1f))
                // Instantiates a random object from the resources pool.
                Instantiate(biome.resourcesPool[Random.Range(0, biome.resourcesPool.Count)], spawnCenter + randomPos, Quaternion.identity);
        }
    }
}
