using UnityEngine;
using System.Collections;

public class IntroMenuController : MonoBehaviour {

    public string MainGameScene;

    public bool playAudio = false;

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

        
    }

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {

    }
}
