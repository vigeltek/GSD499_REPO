using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject score;
    public GameObject wave;
    public GameObject cash;
    private GameController gameController;

    public GameObject laserButton;
    public GameObject rocketButton;
    public GameObject lightningButton;

    public Text laserButtonToolTip;
    public Text rocketButtonToolTip;
    public Text lightningButtonToolTip;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        
    }

    void Awake()
    {
        laserButtonToolTip.enabled = false;
        rocketButtonToolTip.enabled = false;
        lightningButtonToolTip.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        cash.GetComponent<Text>().text = gameController.GetCash().ToString();
        score.GetComponent<Text>().text = gameController.GetScore().ToString();

        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.hovered.Count>0)
        {
            foreach (GameObject g in eventData.hovered) {
                if (g == laserButton) {
                    // laserButtonToolTip.renderer.
                    laserButtonToolTip.enabled = true;
                }

                if (g == rocketButton)
                    rocketButtonToolTip.enabled = true;


                if (g == lightningButton)
                    lightningButtonToolTip.enabled = true;

            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (GameObject g in eventData.hovered)
        {
            if (g == laserButton)
                laserButtonToolTip.enabled = false;

            if (g == rocketButton)
                rocketButtonToolTip.enabled = false;


            if (g == lightningButton)
                lightningButtonToolTip.enabled = false;

        }
    }
}
