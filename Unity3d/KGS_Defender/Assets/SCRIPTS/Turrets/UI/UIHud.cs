using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIHud : MonoBehaviour {
    public Text txtLife;
    public Text txtLevel;
    public UnityButton buttonSpawn;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPauseButton()
    {
        Debug.Log("OnPauseButton Clicked");
        _GameState gameState = GameController.GetGameState();
        if (gameState == _GameState.Over) return;
        
        if (gameState == _GameState.Pause)
        {
            GameController.ResumeGame();
            UIPauseMenu.Hide();
        }
        else
        {
            GameController.PauseGame();
            UIPauseMenu.Show();
        }
    }
}
