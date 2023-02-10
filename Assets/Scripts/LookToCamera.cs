using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LookToCamera : MonoBehaviour
{
    private GameObject MainCamera;
    void Awake()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var n = Camera.main.transform.position + transform.position; 
        transform.rotation = Quaternion.LookRotation(-MainCamera.transform.position);
        Debug.DrawLine(transform.position, Camera.main.transform.position, Color.red);
    }
}
