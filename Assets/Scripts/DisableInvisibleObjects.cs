using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisableInvisibleObjects : MonoBehaviour
{
    public Camera camera;
    public GameObject[] exceptionList;

    void Start()
    {
        if (camera == null)
            camera = Camera.main;
    }

    void Update()
    {
        foreach (GameObject child in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (!IsException(child.gameObject))
            {
                if (!IsRendered(child.transform, camera))
                    child.GetComponent<Renderer>().enabled = false;
                else
                    child.GetComponent<Renderer>().enabled = true;
            }
        }
    }

    private bool IsRendered(Transform objectTransform, Camera camera)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(objectTransform.position);
        return (screenPoint.z > -2 && screenPoint.x > -2 && screenPoint.x < 3 && screenPoint.y > -2 && screenPoint.y < 3);
    }

    private bool IsException(GameObject gameObject)
    {
        foreach (var exception in exceptionList)
        {
            if (exception == gameObject)
                return true;
        }
        return false;
    }
}