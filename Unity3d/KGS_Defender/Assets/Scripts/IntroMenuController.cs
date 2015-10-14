using UnityEngine;
using System.Collections;

public class IntroMenuController : MonoBehaviour {

    public string MainGameScene;

    public bool playAudio = false;

    GameObject MenuMain;
    GameObject TitleText;

    GameObject Credits;


    public void onStartGameClick()
    {

        Application.LoadLevel(MainGameScene);
    }

    public void onExitGameClick()
    {
        Application.Quit();
    }

    public void onCreditsClick()
    {
        if (MenuMain != null && TitleText != null && Credits != null)
        {
            MenuMain.SetActive(false);
            TitleText.SetActive(false);
            Credits.SetActive(true);
        }
        
    }

    public void onButtonbackClick()
    {
        if (MenuMain != null && TitleText != null && Credits != null)
        {
            Credits.SetActive(false);
            MenuMain.SetActive(true);
            TitleText.SetActive(true);
        }
        
    }


    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {

    }

    void Awake()
    {
        MenuMain = GameObject.Find("PanelGameOver");
        TitleText = GameObject.Find("TitleText");
        Credits = GameObject.Find("AnchorCenterCredits");

        if (MenuMain != null && TitleText != null && Credits != null)
        {
            Credits.SetActive(false);
            MenuMain.SetActive(true);
            TitleText.SetActive(true);
        }
    }
}
