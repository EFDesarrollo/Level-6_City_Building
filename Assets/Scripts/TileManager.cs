using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [Header("Button Options")]
    public GameObject ButonPrefab;
    public GameObject ButtonParent;
    public Vector3 leftButtonPos, rigthButtonPos, frontButtonPos, backButtonPos;
    [Header("Proximity Dettection")]
    public LayerMask groundLayer;
    public bool leftCollision;
    public bool rigthCollision, frontCollision, backCollision;
    [Header("Gizmos Options")]
    public float gizmosSize;

    private GameObject leftButton, rigthButton, frontButton, backButton;
    public Vector3[] randPositions = new Vector3[9];
    // Start is called before the first frame update
    void Start()
    {
        InstantiateTileButtons();
        GetNineRandomPosition();
        InstantiateTileObjects();
    }

    // Update is called once per frame
    void Update()
    {
        leftCollision = Physics.Raycast(transform.position, Vector3.left, transform.localScale.x, groundLayer);
        rigthCollision = Physics.Raycast(transform.position, Vector3.right, transform.localScale.x, groundLayer);
        frontCollision = Physics.Raycast(transform.position, Vector3.forward, transform.localScale.x, groundLayer);
        backCollision = Physics.Raycast(transform.position, Vector3.back, transform.localScale.x, groundLayer);

        ButtonViewUpdate();
    }
    void InstantiateTileButtons()
    {
        leftButton = Instantiate(ButonPrefab, ButtonParent.transform);
        leftButton.transform.SetLocalPositionAndRotation(leftButtonPos, Quaternion.identity);
        leftButton.GetComponent<TileButtonManager>().SetButtonValues(transform, Vector3.left);
        leftButton.gameObject.name = "leftButton";
        rigthButton = Instantiate(ButonPrefab, ButtonParent.transform);
        rigthButton.transform.SetLocalPositionAndRotation(rigthButtonPos, Quaternion.identity);
        rigthButton.GetComponent<TileButtonManager>().SetButtonValues(transform, Vector3.right);
        rigthButton.gameObject.name = "rigthButton";
        frontButton = Instantiate(ButonPrefab, ButtonParent.transform);
        frontButton.transform.SetLocalPositionAndRotation(frontButtonPos, Quaternion.identity);
        frontButton.GetComponent<TileButtonManager>().SetButtonValues(transform, Vector3.forward);
        frontButton.gameObject.name = "frontButton";
        backButton = Instantiate(ButonPrefab, ButtonParent.transform);
        backButton.transform.SetLocalPositionAndRotation(backButtonPos, Quaternion.identity);
        backButton.GetComponent<TileButtonManager>().SetButtonValues(transform, Vector3.back);
        backButton.gameObject.name = "backButton";
    }
    void ButtonViewUpdate()
    {
        if (leftCollision)
        {
            leftButton.SetActive(false);
        }
        if (frontCollision)
        {
            frontButton.SetActive(false);
        }
        if (rigthCollision)
        {
            rigthButton.SetActive(false);
        }
        if (backCollision)
        {
            backButton.SetActive(false);
        }
    }

    void GetNineRandomPosition()
    { 
        bool repeated = false;
        do
        {
            print("get tree pos (While)");
            for (int i = 0; i < randPositions.Length; i++)
            {
                print("set tree pos (for i="+i+")");
                randPositions[i] = new Vector3(Mathf.CeilToInt(Random.Range(0, transform.localScale.x)), 1, Mathf.CeilToInt(Random.Range(0, transform.localScale.z)));
                for (int j = 0; j < randPositions.Length; j++)
                {
                    print("i = " + i + ", j = " + j);
                    if (randPositions[i] != randPositions[j] && i != j)
                        repeated = false;
                    if (randPositions[i] == randPositions[j] && i != j)
                        repeated = true;
                }
            }
        } while (repeated);
    }
    void InstantiateTileObjects()
    {

        List<GameObject> objs = ProceduralTerrainGeneratorManager.instance.Objects;
        foreach (var pos in randPositions)
        {
            Instantiate(objs[Random.Range(0, objs.Count)], (transform.position - new Vector3(Mathf.CeilToInt(transform.localScale.x / 2), 0, Mathf.CeilToInt(transform.localScale.z / 2))) + pos, Quaternion.identity);
        }
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
