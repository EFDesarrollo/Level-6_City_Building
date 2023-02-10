using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrainGeneratorManager : MonoBehaviour
{
    static public ProceduralTerrainGeneratorManager instance;
    /// <summary>
    /// determines the size of each of the tiles that will be generated.
    /// </summary>
    public int TileSize = 1;
    /// <summary>
    /// determines the position on the y axis at which the tiles will align.
    /// </summary>
    public int alingPositionOnY = -1;
    /// <summary>
    /// contiene los diferentes biomas y sus probabilidades.
    /// </summary>
    public List<Biome> biomes;
    //public List<GameObject> GroundTiles;
    public List<GameObject> Objects;

    private void Awake()
    {
        // allows access to this class from other scripts.
        instance = this;
    }
    /// <summary>
    /// The "AddTerrainTile" function allows adding a new piece of terrain at a specific position and direction.
    /// The new position is calculated based on the specified base position and direction, and then the 
    /// Instantiate function is used to create a new piece of terrain at the calculated position. 
    /// The terrain tile is randomly selected from the list of GroundTiles.
    /// </summary>
    /// <param name="curPos"></param>
    /// <param name="direcction"></param>
    public void AddTerrainTile(Vector3 curPos, Vector3 direcction)
    {
        // calculating the position of the new piece of terrain
        Vector3 newTilePos = new Vector3(curPos.x + direcction.x, alingPositionOnY, curPos.z + direcction.z);
        if (Vector3.left == direcction)
            newTilePos += new Vector3(-(TileSize - 1), 0, 0);
        if (Vector3.right == direcction)
            newTilePos += new Vector3((TileSize - 1), 0, 0);
        if (Vector3.forward == direcction)
            newTilePos += new Vector3(0, 0, (TileSize - 1));
        if (Vector3.back == direcction)
            newTilePos += new Vector3(0, 0, -(TileSize - 1));

        int randomValue = Random.Range(0, 100);
        int cumulativePercentage = 0;
        foreach (Biome biome in biomes)
        {
            cumulativePercentage += biome.probability;
            if (randomValue <= cumulativePercentage)
            {
                GameObject tile = Instantiate(biome.tilePrefab, newTilePos, Quaternion.identity);
                break;
            }
        }
    }
    /// <summary>
    /// Biome class that has two properties: tilePrefab and probability.
    /// The tilePrefab property is a game object that represents the biome,
    /// and the probability property is the probability of appearance of the biome in percentage
    /// (for example, if probability is 25, it means that the biome will appear in 25% of the cases).
    /// </summary>
    [System.Serializable]
    public class Biome
    {
        public GameObject tilePrefab;
        public int probability;
    }
}
