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


    public GameObject endUI;
    public Text endMessage;

    public static BuildManager Instance;

    public int mana = 100;

    public GameObject upgradeTowerCanvas;

    private Animator upgradeTowerCanvasAnimator;

    public Button upgradeButton;

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
                    if (selectedTowerData != null && ground.towerGo == null)
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
                    else if (ground.towerGo !=null)
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
                }
                else
                    return;
            }
            else if (EventSystem.current.IsPointerOverGameObject())
            {
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
    
    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "Victory";
    }
    
    public void Defeat()
    {
        endUI.SetActive(true);
        endMessage.text = "Defeat";
        
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
        Debug.Log("");
        selectedGround.UpgradeTower();
        StartCoroutine(HideUpgradeUI());
    }

    //Destroy tower function
    public void OnDestroyButtonDown()
    {
        selectedGround.DestroyTower();
        selectedGround.isUpgraded = false;
        StartCoroutine(HideUpgradeUI());
    }

}
