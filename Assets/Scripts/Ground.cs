using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject towerGo;//save current tower that on the ground

    //Variables for the highlight of the ground cube
    [SerializeField] private Color cubeColor;
    private Color startColor;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void BuildTower(GameObject towerPrefab)
    {
        towerGo = (GameObject)Instantiate(towerPrefab, transform.position, Quaternion.identity);
    }

    //Change the color of the grass cube that the mouse points at
    private void OnMouseEnter()
    {
        rend.material.color = cubeColor;
    }

    //Reset the color when the mouse point leaves
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
