using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIHud : MonoBehaviour {
    public Text txtLife;
    public Text txtLevel;
    public UnityButton buttonSpawn;
    public static bool upgradeMenuShowing = false;
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
            if (upgradeMenuShowing)
            {
                upgradeMenuShowing = false;
                TPManager.showUpgradeMenu(true);
            }
        }
        else
        {
            if (TPManager.isUpgradeMenuActive())
            {
                upgradeMenuShowing = true;
                TPManager.showUpgradeMenu(false);
            }
            GameController.PauseGame();
            UIPauseMenu.Show();
        }
    }
}
