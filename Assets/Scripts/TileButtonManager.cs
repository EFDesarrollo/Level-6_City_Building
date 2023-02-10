using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileButtonManager : MonoBehaviour
{
    public Transform tileTransform;
    public Vector3 direction;

    public void OnClickAddTile()
    {
        ProceduralTerrainGeneratorManager.instance.AddTerrainTile(tileTransform.position, direction);
    }
    public void SetButtonValues(Transform tileT, Vector3 tileDirection)
    {
        tileTransform = tileT;
        direction = tileDirection;
    }
}
