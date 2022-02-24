using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerData
{
    public GameObject towerPrefab;
    public int cost;
    public GameObject TowerUpgradedPrefab;
    public int UpgradedCost;
    public TowerType type;    
}
public enum TowerType
{
    TowerIce,
    TowerEarth,
    TowerCrystal,
    TowerFire
}
