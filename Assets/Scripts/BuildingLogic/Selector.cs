using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Selector : MonoBehaviour
{
    private Camera cam;

    public static Selector instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    //returns the tile that the mouse is hovering over
    public Vector3 GetCurTilePosition()
    {
        //return if we've hovering over UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return new Vector3(0, -99, 9);
        }

        //create the plane, ray and out distance
        Plane plane = new Plane(Vector3.up, Vector3.zero);  
        // TODO: Aqui hay un Ray y en buildingPlacement hay otro Ray
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //float rayOut = 0.0f;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //get the position at which we intersected the plane
            Vector3 newPos = hit.point - new Vector3(0.5f, 0.0f, 0.5f);

            /*print("Original " + newPos.x + " + " + newPos.z);
            print("CeilToInt " + Mathf.CeilToInt(newPos.x) + " + " + Mathf.CeilToInt(newPos.z));
            print("FloorToInt " + Mathf.FloorToInt(newPos.x) + " + " + Mathf.FloorToInt(newPos.z));*/
            //round that up to the nearest full number (nearest meter)
            newPos = new Vector3(Mathf.CeilToInt(newPos.x),1f, Mathf.CeilToInt(newPos.z));

            return newPos;
        }

        return new Vector3(0, -99, 9);
    }
}
