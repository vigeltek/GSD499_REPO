using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TPManager : MonoBehaviour {


    public int laserCost;
    public int rocketCost;
    public int lightningCost;
    public int laserSellPrice;
    public int rocketSellPrice;
    public int lightningSellPrice;

    public GameObject laserTowerPrefab;
    public GameObject rocketTowerPrefab;
    public GameObject lightningTowerPrefab;

    public Color highlightColor;
    public Color normalColor;
    public Color invalidColor;

    public GameObject currentPrefab;
    int currCost;

    public bool placementMode;
    public bool sellMode;
    public bool upgradeMode;

    public UnityEngine.UI.Text buildModeText;
    public GameObject laserButton;
    public GameObject rocketButton;
    public GameObject lightningButton;
    public GameObject sellButton;
    public GameObject upgradeButton;

    public Color activeColor;
    public Color disabledColor;

    private static TPManager instance;
    private GameObject thisObj;

    public GameController GM;
    GameObject turrGrid;

    public GameObject UpgradeGUI;
    public Text upgradeCostGUIText;
    public Button upgradeGUIConfirmButton;
    public Text textTitle;
    string textTitleInitialValue;

    public static void Init()
    {
        if (instance != null) return;

    }

    public static void BuildLaserTurretHotKey()
    {
        instance.ChangeActiveTurret(1);
    }

    public static void BuildRocketTurretHotKey()
    {
        instance.ChangeActiveTurret(2);
    }

    public static void BuildLightningTurretHotKey()
    {
        instance.ChangeActiveTurret(3);
    }

    public static void SellTurretHotKey()
    {
        instance.SellMode();
    }

    public static void UpgradeTurretHotKey()
    {
        instance.UpgradeMode();
    }

    public static void CancelHotKey()
    {
        instance.CancelCurrentAction();
    }

    void Start()
    {
        textTitleInitialValue = textTitle.text;
    }
    // Use this for initialization
    void Awake () {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        thisObj = gameObject;


        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        turrGrid = GameObject.FindGameObjectWithTag("PanelPlacement");
    }
	
	// Update is called once per frame
	void Update () {

        //check to see if turret can be purchased.
        float cash = GM.GetCash();

        if (cash >= laserCost)
        {
            laserButton.GetComponent<Button>().interactable = true;
            //laserButton.GetComponentInChildren<Text>().color = activeColor;
        }
        else
        {
            laserButton.GetComponent<Button>().interactable = false;
            //laserButton.GetComponentInChildren<Text>().color = disabledColor;
        }
        if (cash >= rocketCost)
        {
            rocketButton.GetComponent<Button>().interactable = true;
            //rocketButton.GetComponentInChildren<Text>().color = activeColor;
        }
        else
        {
            rocketButton.GetComponent<Button>().interactable = false;
            //rocketButton.GetComponentInChildren<Text>().color = disabledColor;
        }
        if (cash >= lightningCost)
        {
            lightningButton.GetComponent<Button>().interactable = true;
            //lightningButton.GetComponentInChildren<Text>().color = activeColor;

        }
        else
        {
            lightningButton.GetComponent<Button>().interactable = false;
            //lightningButton.GetComponentInChildren<Text>().color = disabledColor;
        }

    }

    public bool canAfford()
    {
        if(GM.GetCash() >= currCost)
        {
            return true;
        }
        return false;
    }
    public GameObject GetTurret()
    {
        GM.SpendCash(currCost);
        return currentPrefab;
    }

    public void ChangeActiveTurret(int selection)
    {
        if(placementMode)
        {
            PlacementMode();
        }
        if (!placementMode)
        {
            PlacementMode();
        }

        if(selection == 1)
        {
            currentPrefab = laserTowerPrefab;
            currCost = laserCost;
        }
        if (selection == 2)
        {
            currentPrefab = rocketTowerPrefab;
            currCost = rocketCost;
        }
        if (selection == 3)
        {
            currentPrefab = lightningTowerPrefab;
            currCost = lightningCost;
        }
    }

    public void SellTurret(int selection)
    {
        if (selection == 1)
        {
            GM.AddResource(laserSellPrice);
        }
        if (selection == 2)
        {
            GM.AddResource(rocketSellPrice);
        }
        if (selection == 3)
        {
            GM.AddResource(lightningSellPrice);
        }
    }

    public void CancelCurrentAction()
    {
        if (instance.sellMode)
        {
            instance.sellMode = false;
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }

        if (instance.placementMode)
        {
            instance.placementMode = false;
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }


        if (instance.upgradeMode)
        {
            instance.upgradeMode = false;
            UpgradeGUI.SetActive(false);
            EnableButtons();
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }

    }

    public void SellMode()
    {
        if (instance.sellMode)
        {
            instance.sellMode = false;
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }
        else
        {
            instance.placementMode = false;
            instance.upgradeMode = false;
            instance.sellMode = true;
            instance.turrGrid.BroadcastMessage("GridVisibility", true);
        }
            
            

    }

    public static bool isUpgradeMenuActive()
    {
        return instance.UpgradeGUI.activeSelf;
    }
    public static void showUpgradeMenu(bool bShow)
    {
        instance.UpgradeGUI.SetActive(bShow);       
    }


    public void UpgradeMode() {
        if (instance.upgradeMode)
        {
            instance.upgradeMode = false;
            UpgradeGUI.SetActive(false);
            EnableButtons();
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }
        else
        {
            instance.placementMode = false;
            instance.upgradeMode = true;
            instance.sellMode = false;
            instance.turrGrid.BroadcastMessage("GridVisibility", true);
        }


    }

    /*
    public void TurnOnGrid()
    {
        instance.upgrade = true;
        instance.turrGrid.BroadcastMessage("GridVisibility", true);
    }
    */
    public void TurnOffGrid()
    {
        instance.upgradeMode = false;
        UpgradeGUI.SetActive(false);
        EnableButtons();
        instance.turrGrid.BroadcastMessage("GridVisibility", false);


    }
   


    public static void PlacementMode()
    {
        if (instance.placementMode)
        {
            instance.placementMode = false;
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }
        else
        {
            //Debug.Log("Turn On Grid");
            instance.placementMode = true;
            instance.sellMode = false;
            instance.upgradeMode = false;
            instance.turrGrid.BroadcastMessage("GridVisibility", true);
        }

    }

    void DisableButtons()
    {

        laserButton.GetComponent<Button>().interactable = false;      
        rocketButton.GetComponent<Button>().interactable = false;
        lightningButton.GetComponent<Button>().interactable = false;
        sellButton.GetComponent<Button>().interactable = false;
        upgradeButton.GetComponent<Button>().interactable = false;
    }

    public void EnableButtons()
    {

        laserButton.GetComponent<Button>().interactable = true;
        rocketButton.GetComponent<Button>().interactable = true;
        lightningButton.GetComponent<Button>().interactable = true;
        sellButton.GetComponent<Button>().interactable = true;
        upgradeButton.GetComponent<Button>().interactable = true;
    }

    public void Upgrade(placementPanel panel, TurretUpgrade tu)
    {
        //Set the reference for the panel asking to upgrade
        UpgradeGUI.GetComponent<GUIRef>().GUIReference = panel;
        upgradeCostGUIText.text = "$" + tu.CostOfUpgrade().ToString();

        if (GM.GetCash() >= tu.CostOfUpgrade())
        {
            if (tu.CostOfUpgrade() == 0)
            {
                textTitle.text = "This turret is Max Level";
                upgradeCostGUIText.text = "";
                upgradeGUIConfirmButton.interactable = false;
            }
            else
            {
                textTitle.text = textTitleInitialValue;
                upgradeGUIConfirmButton.interactable = true;
            }
        }
        else
        {
            upgradeGUIConfirmButton.interactable = false;
        }

        DisableButtons();
        UpgradeGUI.SetActive(true);

    }

}
