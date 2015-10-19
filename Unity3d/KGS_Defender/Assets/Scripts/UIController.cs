using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject score;
    public GameObject wave;
    public GameObject cash;
    GameManager GM;

    public GameObject laserButton;
    public GameObject rocketButton;
    public GameObject lightningButton;

    public Text laserButtonToolTip;
    public Text rocketButtonToolTip;
    public Text lightningButtonToolTip;

    // Use this for initialization
    void Start ()
    {
        GM = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        
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
        cash.GetComponent<Text>().text = GM.GetCash().ToString();
        score.GetComponent<Text>().text = GM.GetScore().ToString();

        
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
