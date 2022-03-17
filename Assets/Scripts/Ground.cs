using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject towerGo;//save current tower that on the ground
    [HideInInspector]
    public TowerData towerData;
    [HideInInspector]
    public bool isUpgraded = false;

    //Variables for the highlight of the ground cube
    [SerializeField] private Color cubeColor;
    private Color startColor;
    private Renderer rend;

    //Varaible for the VFX
    public GameObject buildFX;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void BuildTower(TowerData towerData)
    {
        this.towerData = towerData;
        isUpgraded = false;
        towerGo = (GameObject)Instantiate(towerData.towerPrefab, transform.position, Quaternion.identity); //Instantiate tower
        GameObject effect = (GameObject)Instantiate(buildFX, transform.position, Quaternion.identity); //Instantiate build VFX
        Destroy(effect, 1f); //Remove the VFX after 1 sec

    }

    public void UpgradeTower()
    {
        if (isUpgraded == true) return;

        Destroy(towerGo);
        isUpgraded = true;
        towerGo = (GameObject)Instantiate(towerData.TowerUpgradedPrefab, transform.position, Quaternion.identity);
    }

    public void DestroyTower()
    {
        Destroy(towerGo);
        isUpgraded = false;
        towerGo = null;
        towerData = null;

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
