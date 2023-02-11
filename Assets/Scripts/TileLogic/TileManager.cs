using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Biome biome;

    // Variables for the button options
    /// <summary>
    /// The prefab for the button
    /// </summary>
    [Header("Button Options")]
    public GameObject ButonPrefab;
    /// <summary>
    /// The parent object for all the buttons
    /// </summary>
    public GameObject ButtonParent;
    /// <summary>
    /// Position for the button
    /// </summary>
    public Vector3 leftButtonPos, rigthButtonPos, frontButtonPos, backButtonPos;

    // Variables for the proximity detection
    /// <summary>
    /// The layer used for detecting the ground
    /// </summary>
    [Header("Proximity Dettection")]
    public LayerMask groundLayer;
    /// <summary>
    /// Boolean for detecting if there is a collision on the left side
    /// </summary>
    public bool leftCollision;
    /// <summary>
    /// Booleans for detecting if there is a collision on the right side
    /// </summary>
    public bool rigthCollision;
    /// <summary>
    /// Booleans for detecting if there is a collision on the front side
    public bool frontCollision;
    /// <summary>
    /// Booleans for detecting if there is a collision on the back side
    /// </summary>
    public bool backCollision;
    
    // Variables for the gizmos options
    /// <summary>
    /// The size of the gizmos spheres
    /// </summary>
    [Header("Gizmos Options")]
    public float gizmosSize;

    // Private Variables
    private GameObject leftButton, rigthButton, frontButton, backButton;


    // Start is called before the first frame update
    void Start()
    {
        InstantiateTileButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for collisions in each direction
        leftCollision = Physics.Raycast(transform.position, Vector3.left, transform.localScale.x, groundLayer);
        rigthCollision = Physics.Raycast(transform.position, Vector3.right, transform.localScale.x, groundLayer);
        frontCollision = Physics.Raycast(transform.position, Vector3.forward, transform.localScale.x, groundLayer);
        backCollision = Physics.Raycast(transform.position, Vector3.back, transform.localScale.x, groundLayer);

        // Update the visibility of the buttons based on the collisions
        ButtonViewUpdate();
    }

    // Function to instantiate the buttons
    /// <summary>
    /// This function is used to instantiate 4 buttons, one for each direction, left, right, front, and back, 
    /// and sets their positions and directions.
    /// </summary>
    void InstantiateTileButtons()
    {
        // Arrays with the positions, directions and names for each button
        Vector3[] buttonPositions = new Vector3[] { leftButtonPos, rigthButtonPos, frontButtonPos, backButtonPos };
        Vector3[] directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        string[] buttonNames = new string[] { "leftButton", "rigthButton", "frontButton", "backButton" };

        // Array to store the buttons after they are instantiated
        GameObject[] buttons = new GameObject[4];

        // Loop to instantiate the buttons
        for (int i = 0; i < 4; i++)
        {
            // Instantiating a button prefab and setting its parent as the "ButtonParent" object
            buttons[i] = Instantiate(ButonPrefab, ButtonParent.transform);
            // Setting the position and rotation of the button
            buttons[i].transform.SetLocalPositionAndRotation(buttonPositions[i], Quaternion.identity);
            // Setting the values for the TileButtonManager component on the button
            buttons[i].GetComponent<TileButtonManager>().SetButtonValues(transform, directions[i]);
            // Setting the name for the button
            buttons[i].gameObject.name = buttonNames[i];
        }

        // Assigning the buttons to the respective variables
        leftButton = buttons[0];
        rigthButton = buttons[1];
        frontButton = buttons[2];
        backButton = buttons[3];
    }

    // Function to update the visibility of buttons
    /// <summary>
    /// This function updates the visibility of the buttons based on the collision detected in each direction. 
    /// If a collision is detected, the corresponding button is disabled.
    /// </summary>
    void ButtonViewUpdate()
    {
        // Disable button if collision is detected in corresponding direction
        leftButton.SetActive(!leftCollision);
        frontButton.SetActive(!frontCollision);
        rigthButton.SetActive(!rigthCollision);
        backButton.SetActive(!backCollision);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Left button position
        Gizmos.DrawWireSphere(ButtonParent.transform.position + leftButtonPos, gizmosSize);
        // Front button position
        Gizmos.DrawWireSphere(ButtonParent.transform.position + frontButtonPos, gizmosSize);
        // Rigth button position
        Gizmos.DrawWireSphere(ButtonParent.transform.position + rigthButtonPos, gizmosSize);
        // Back button position
        Gizmos.DrawWireSphere(ButtonParent.transform.position + backButtonPos, gizmosSize);
        // Left Tile Detect
        Gizmos.DrawRay(transform.position, Vector3.left * transform.localScale.x);
        // Front Tile Detect
        Gizmos.DrawRay(transform.position, Vector3.forward * transform.localScale.x);
        // Right Tile Detect
        Gizmos.DrawRay(transform.position, Vector3.right * transform.localScale.x);
        // Back Tile Detect
        Gizmos.DrawRay(transform.position, Vector3.back * transform.localScale.x);

    }
}
