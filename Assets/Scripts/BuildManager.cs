using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public TowerData towerIceData;
    public TowerData towerEarthData;
    public TowerData towerCrystalData;
    public TowerData towerFireData;


    public Text manaText;

    public Animator manaanimator;


    public int mana = 100;
    //Express current tower selection(the tower that want to build)
    private TowerData selectedTowerData;

    void ChangeMana(int change=0)
    {
        mana += change;
        manaText.text = " " + mana;
    }



    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("Grass"));
                if(isCollider)
                {
                    Grass Grass = hit.collider.GetComponent<Grass>();
                    if(Grass.towerGo == null)
                    {
                        //Can build
                        if(mana > selectedTowerData.cost)
                        {
                            ChangeMana(-selectedTowerData.cost);
                            Grass.BuildTower(selectedTowerData.towerPrefab);
                        }
                        else
                        {
                            //Need more mana
                            manaanimator.SetTrigger("Flicker");
                        }
                    }
                    else
                    {
                        //upgrade
                    }
                }
            }
        }
    }

    public void OnIceSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTowerData = towerIceData;
        }
    }
    public void OnEarthSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTowerData = towerEarthData;
        }
    }
    public void OnCrystalSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTowerData = towerCrystalData;
        }
    }
    public void OnFireSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTowerData = towerFireData;
        }
    }
    

    




}
