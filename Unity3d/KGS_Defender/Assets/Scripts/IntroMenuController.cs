using UnityEngine;
using System.Collections;

public class IntroMenuController : MonoBehaviour {

    public string MainGameScene;

    public bool playAudio = false;

    GameObject MenuMain;
    GameObject TitleText;

    GameObject CreditsDialog;


    public void onStartGameClick()
    {

        Application.LoadLevel(2);
        
    }

    public void onStartMenuClick()
    {

        Application.LoadLevel(1);

    }

    public void onExitGameClick()
    {
        Application.Quit();
    }

    public void onCreditsClick()
    {

        Application.LoadLevel(3);
        /*
        if (MenuMain != null && TitleText != null && CreditsDialog != null)
        {
            MenuMain.SetActive(false);
            TitleText.SetActive(false);
            CreditsDialog.SetActive(true);

            
        }
        */
        
    }

    public void onButtonbackClick()
    {
        if (MenuMain != null && TitleText != null && CreditsDialog != null)
        {
            CreditsDialog.SetActive(false);
            MenuMain.SetActive(true);
            TitleText.SetActive(true);
        }
        
    }


    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void Awake()
    {
        MenuMain = GameObject.Find("PanelGameOver");
        TitleText = GameObject.Find("TitleText");
        CreditsDialog = GameObject.Find("AnchorCenterCredits");

        if (MenuMain != null && TitleText != null && CreditsDialog != null)
        {
            CreditsDialog.SetActive(false);
            MenuMain.SetActive(true);
            TitleText.SetActive(true);
        }
    }
}
