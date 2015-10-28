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

    public UnityEngine.UI.Text buildModeText;
    public GameObject laserButton;
    public GameObject rocketButton;
    public GameObject lightningButton;
    public GameObject SellButton;

    public Color activeColor;
    public Color disabledColor;

    private static TPManager instance;
    private GameObject thisObj;

    GameController GM;
    GameObject turrGrid;

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

    public static void CancelHotKey()
    {
        instance.CancelCurrentAction();
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
            instance.sellMode = true;
            instance.turrGrid.BroadcastMessage("GridVisibility", true);
        }
            
            

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
            instance.turrGrid.BroadcastMessage("GridVisibility", true);
        }

    }

    void DisableButtons()
    {

        laserButton.GetComponent<Button>().interactable = false;      
        rocketButton.GetComponent<Button>().interactable = false;
        lightningButton.GetComponent<Button>().interactable = false;
    }
    
}
