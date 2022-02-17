using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    [HideInInspector]
    public GameObject towerGo;//save current tower that on the grass

    public void BuildTower(GameObject towerPrefab)
    {
        towerGo = GameObject.Instantiate(towerPrefab, transform.position, Quaternion.identity);
    }

}
