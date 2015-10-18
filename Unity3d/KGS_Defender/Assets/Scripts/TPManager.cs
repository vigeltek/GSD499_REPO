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
    public bool destroyMode;

    public UnityEngine.UI.Text buildModeText;
    public GameObject laserButton;
    public GameObject rocketButton;
    public GameObject lightningButton;
    public GameObject SellButton;

    public Color activeColor;
    public Color disabledColor;

    private static TPManager instance;
    private GameObject thisObj;

    GameManager GM;
    GameObject turrGrid;

    public static void Init()
    {
        if (instance != null) return;

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


        GM = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        turrGrid = GameObject.FindGameObjectWithTag("PanelPlacement");
    }
	
	// Update is called once per frame
	void Update () {

        //check to see if turret can be purchased.
        int cash = GM.GetCash();

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
    
    public static void PlacementMode()
    {
        if (instance.placementMode)
        {
            instance.placementMode = false;
            instance.buildModeText.color = instance.activeColor;
            instance.buildModeText.text = "Build";
            //DisableButtons();
            instance.turrGrid.BroadcastMessage("GridVisibility", false);
        }
        else
        {
            instance.placementMode = true;
            // EnableButtons();
            instance.buildModeText.color = instance.disabledColor;
            instance.buildModeText.text = "OFF";
            instance.turrGrid.BroadcastMessage("GridVisibility", true);
        }

    }

    void DisableButtons()
    {

        laserButton.GetComponent<Button>().interactable = false;      
        rocketButton.GetComponent<Button>().interactable = false;
        lightningButton.GetComponent<Button>().interactable = false;
        //  SellButton.interactable = true;

        //laserButton.GetComponentInChildren<Text>().color = disabledColor;
        //rocketButton.GetComponentInChildren<Text>().color = disabledColor;
        //lightningButton.GetComponentInChildren<Text>().color = disabledColor;
    }

    /*
    void EnableButtons()
    {
        laserButton.GetComponent<Button>().interactable = true;
        rocketButton.GetComponent<Button>().interactable = true;
        lightningButton.GetComponent<Button>().interactable = true;
        //   SellButton.interactable = false;
        laserButton.GetComponentInChildren<Text>().color = activeColor;
        rocketButton.GetComponentInChildren<Text>().color = activeColor;
        lightningButton.GetComponentInChildren<Text>().color = activeColor;
       
    }
    */


}
