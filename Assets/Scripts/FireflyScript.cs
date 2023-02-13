using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyScript : MonoBehaviour
{
    public int numberOfFireflies;
    public GameObject fireflyPrefab;
    public float spawnRadius;
    public Color fireflyColor;

    private GameObject[] fireflies;
    
    private void Start()
    {
        // Instanciamos el n�mero de luci�rnagas especificado
        fireflies = new GameObject[numberOfFireflies];
        for (int i = 0; i < numberOfFireflies; i++)
        {
            Vector3 spawnPosition = Random.onUnitSphere * spawnRadius;
            fireflies[i] = Instantiate(fireflyPrefab, spawnPosition, Quaternion.identity, transform);
            ChangeColor(fireflies[i]);
        }
    }

    // Funci�n que accede al material y cambia el color en la instanciaci�n
    private void ChangeColor(GameObject firefly)
    {
        Renderer renderer = firefly.GetComponent<Renderer>();
        renderer.material.SetColor("_Color", fireflyColor);
    }

    // Funci�n que desactiva la visualizaci�n de las luci�rnagas
    public void DisableFireflies()
    {
        for (int i = 0; i < numberOfFireflies; i++)
        {
            fireflies[i].SetActive(false);
        }
    }

    // Funci�n que Activa la visualizaci�n de las luci�rnagas
    public void AbleFireflies()
    {
        for (int i = 0; i < numberOfFireflies; i++)
        {
            fireflies[i].SetActive(true);
        }
    }

    // Funci�n que simula el movimiento de las luci�rnagas alrededor del objeto padre
    private void Update()
    {
        for (int i = 0; i < numberOfFireflies; i++)
        {
            Vector3 newPosition = Random.onUnitSphere * spawnRadius + transform.position;
            fireflies[i].transform.position = Vector3.Lerp(fireflies[i].transform.position, newPosition, Time.deltaTime);
        }
    }
}