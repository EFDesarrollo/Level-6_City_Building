using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;

    private float indicatorUpdateRate = 0.05f; //seconds
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozeIndicator;
    public LayerMask groundLayer;
    [SerializeField]
    private bool isOnGround, isOnBuilding;

    #region UI Buttons
    //called when we press a building UI button
    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        CancelBuildingPlacement();
        // make sure we have enough money
        if (!City.instance.CheckEnougthResources(preset))
            return;

        currentlyPlacing = true;
        curBuildingPreset = preset;
        placementIndicator.SetActive(true);

    }


    // called when we press a building UI button, turn bulldoze on off
    public void ToggleBulldoze()
    {
        CancelBuildingPlacement();
        currentlyBulldozering = !currentlyBulldozering;
        bulldozeIndicator.SetActive(currentlyBulldozering);
    }
    #endregion

    #region Keyboard
    //called when we place down a building or press Escape
    private void CancelBuildingPlacement()
    {
        
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
        bulldozeIndicator.SetActive(false);
        currentlyBulldozering = false;
       
    }
    #endregion

    private void Update()
    {
        /// void ray that will contain RayCast's Values
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool rayCollision = Physics.Raycast(ray, out hit, 100);
        isOnGround = hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        isOnBuilding = hit.collider != null && (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Building") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Resource"));

        //cancel building placement
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelBuildingPlacement();

        //called every 0.05 seconds 
        if (Time.time - lastUpdateTime > indicatorUpdateRate)
        {
            lastUpdateTime = Time.time;

            //get the currently selected tile position
            curIndicatorPos = Selector.instance.GetCurTilePosition();

            //move the placement indicator or bulldoze indicator to the selected tile
            if (currentlyPlacing && isOnGround)
                placementIndicator.transform.position = curIndicatorPos;
            else if(currentlyBulldozering && isOnBuilding)
                bulldozeIndicator.transform.position = curIndicatorPos;
        }

        if ((isOnGround || isOnBuilding) && !GUI.changed)
        {
            //called when we press left mouse button
            if (Input.GetMouseButtonDown(0) && currentlyPlacing)
            {
                Vector3 dummyPos = new Vector3(0, -99, 9);
                //if (buildingObj.transform.position != dummyPos)
                    PlaceBuilding();
            }
            else if (Input.GetMouseButtonDown(0) && currentlyBulldozering)
                Bulldoze();
        }
    }

    //deletes the currently selected building
    private void Bulldoze()
    {
        Building buildingToDestroy = City.instance.buildings.Find(x=>x.transform.position == curIndicatorPos);

        if (buildingToDestroy != null)
        {
            City.instance.OnRemoveBuilding(buildingToDestroy);
        }
    }

    //places down the currently selected building
    private void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab, curIndicatorPos, Quaternion.identity);

        City.instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());

        BeginNewBuildingPlacement(curBuildingPreset);
    }
    private void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Gizmos.color = Color.green;

        Gizmos.DrawRay(ray.origin,ray.direction * 100);
    }
}
