using UnityEngine;
using System.Collections;

public class IntroScript : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public bool useExitButton = true;

    public GameObject exitButton;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void onCommand(string str)
    {
        if (str.Equals("Exit"))
        {
            Application.Quit();
        }


        if (str.Equals("Credits"))
        {
            //Constants.fadeInFadeOut(creditsMenu, mainMenu);

        }
        if (str.Equals("CreditsBack"))
        {
            //Constants.fadeInFadeOut(mainMenu, creditsMenu);
        }

    }

    void Awake()
    {
        Screen.SetResolution(1074, 768, true);

        if (useExitButton == false)
        {
            exitButton.SetActive(false);
        }

        
    }
}
