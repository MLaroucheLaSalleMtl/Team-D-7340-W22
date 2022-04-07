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

    //Indicates the currently selected tower(Game objects in the scene)
    private Ground selectedGround;

    public Text manaText;
    public Animator manaanimator;
    public int mana = 100;

    public GameObject upgradeTowerCanvas;
    private Animator upgradeTowerCanvasAnimator;
    public Button upgradeButton;

    //Get tower properties
    public GameObject dataUI;
    public GameObject crystalTower;
    public GameObject iceTower;
    public GameObject fireTower;
    public GameObject earthTower;


    //Variables for tower selection
    private TowerData selectedTowerData; //The selected tower in the build UI
    //private GameObject selectedTowerGo; //The selected tower in the scene

    public void ChangeMana(int change = 0)
    {
        mana += change;
        manaText.text = " " + mana;
    }

    //Singleton pattern
    private void Awake()
    {

        instance = this;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        SelectData();
    }

    public void SelectData()
    {
        dataUI.SetActive(false);
        crystalTower.SetActive(false);
        iceTower.SetActive(false);
        fireTower.SetActive(false);
        earthTower.SetActive(false);
    }

    void Start()
    {
        upgradeTowerCanvasAnimator = upgradeTowerCanvas.GetComponent<Animator>();
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                //Using ray for the mouse click detection
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit; //Ray's target
                bool isCollider = Physics.Raycast(ray,out hit, LayerMask.GetMask("Ground"));
                if (isCollider)
                {
                    Ground ground = hit.collider.GetComponent<Ground>();
                    if (ground == null) //Fix the null reference exception problem when clicking a cube other than ground
                        return;
                    else if (selectedTowerData != null && ground.towerGo == null)
                    {
                        //Can build
                        if (mana >= selectedTowerData.cost)
                        {
                            ChangeMana(-selectedTowerData.cost);
                            ground.BuildTower(selectedTowerData);
                        }
                        else
                        {
                            //Need more mana
                            manaanimator.SetTrigger("Flicker");
                        }
                    }
                    else if (ground.towerGo != null)
                    {
                        //upgrade  
                        ShowUpgradeUI(ground.transform.position, ground.isUpgraded);
                        if (ground == selectedGround && upgradeTowerCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgradeUI(ground.transform.position, ground.isUpgraded);
                        }
                        selectedGround = ground; //Update the selection
                    }
                    else
                        return;
                }
                else
                    return;
            }
        }
    }

    public void OnIceSelected(bool isOn)
    {        
        if (isOn)
        {
            dataUI.SetActive(false);
            dataUI.SetActive(true);
            selectedTowerData = towerIceData;
            //iceTower = GetComponent<GameObject>();
            iceTower.SetActive(true);
            earthTower.SetActive(false);
            crystalTower.SetActive(false);
            fireTower.SetActive(false);
        }
    }
    public void OnEarthSelected(bool isOn)
    {
        if(isOn)
        {
            dataUI.SetActive(false);
            dataUI.SetActive(true);
            selectedTowerData = towerEarthData;
            //earthTower = GetComponent<GameObject>();
            earthTower.SetActive(true); 
            iceTower.SetActive(false);
            crystalTower.SetActive(false);
            fireTower.SetActive(false);
        }
    }
    public void OnCrystalSelected(bool isOn)
    {
        if(isOn)
        {
            dataUI.SetActive(false);
            dataUI.SetActive(true);
            selectedTowerData = towerCrystalData;
            //crystalTower = GetComponent<GameObject>();
            crystalTower.SetActive(true);
            iceTower.SetActive(false);
            earthTower.SetActive(false);
            fireTower.SetActive(false);
        }
    }
    public void OnFireSelected(bool isOn)
    {
        if(isOn)
        {
            dataUI.SetActive(false);
            dataUI.SetActive(true);
            selectedTowerData = towerFireData;
            //fireTower = GetComponent<GameObject>();
            fireTower.SetActive(true);
            iceTower.SetActive(false);
            earthTower.SetActive(false);
            crystalTower.SetActive(false);
        }
    }

    //Upgrade tower UI with hide and show function
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeTowerCanvas.SetActive(false);
        upgradeTowerCanvas.SetActive(true);
        upgradeTowerCanvas.transform.position = pos; //Fix the stuck bug
        upgradeButton.interactable = isActiveAndEnabled;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeTowerCanvasAnimator.SetTrigger("Hide");
        upgradeTowerCanvas.SetActive(false);
        yield return new WaitForSeconds(0.8f);
        //upgradeTowerCanvas.SetActive(false);

    }

    //upgrade tower function
    public void OnUpgradeButtonDown()
    {
        if(mana >= selectedGround.towerData.UpgradedCost)
        {
            ChangeMana(-selectedGround.towerData.UpgradedCost);
            selectedGround.UpgradeTower();
        }
        else
        {
            manaanimator.SetTrigger("Flicker");
        }
        selectedGround.UpgradeTower();
        StartCoroutine(HideUpgradeUI());
    }

    //Destroy tower function
    public void OnDestroyButtonDown()
    {
        if (!selectedGround.isUpgraded)
            ChangeMana(+selectedGround.towerData.cost / 2);
        else
            ChangeMana(+(selectedGround.towerData.cost + selectedGround.towerData.UpgradedCost) / 2);
        selectedGround.DestroyTower();
        selectedGround.isUpgraded = false;
        StartCoroutine(HideUpgradeUI());
    }
}
