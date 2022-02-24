using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    //Variable for singleton pattern
    public static BuildManager instance = null; 

    public TowerData towerIceData;
    public TowerData towerEarthData;
    public TowerData towerCrystalData;
    public TowerData towerFireData;

    public Text manaText;

    public Animator manaanimator;


    public int mana = 100;
    //Express current tower selection(the tower that want to build)
    private TowerData selectedTowerData;

    void ChangeMana(int change = 0)
    {
        mana += change;
        manaText.text = " " + mana;
    }

    //Singleton pattern
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Not clicking UI");

                //Using ray for the mouse click detection
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; //Ray's target
                bool isCollider = Physics.Raycast(ray,out hit, LayerMask.GetMask("Grass"));
                if(isCollider)
                {
                    Grass grass = hit.collider.GetComponent<Grass>();
                    if(selectedTowerData != null && grass.towerGo == null)
                    {
                        //Can build
                        if(mana >= selectedTowerData.cost)
                        {
                            ChangeMana(-selectedTowerData.cost);
                            grass.BuildTower(selectedTowerData.towerPrefab);
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
            else if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicking UI");
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
