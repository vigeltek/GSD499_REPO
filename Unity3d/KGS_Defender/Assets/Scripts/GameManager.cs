using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int cash;
    public int score;
    public GameObject wave;
    private string txt;

    public GameObject ship;
    public GameObject shield;
    private bool gameOver = false;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        GetWave();
        float shipHealth = ship.GetComponent<Stats>().health;
        float sheildHealth = shield.GetComponent<Stats>().health;
        if (sheildHealth <= 0) //shipHealth <= 0 && 
        {
            gameOver = true;
            UIGameOverMenu.Show();
        }
    }
    public int GetScore()
    {
        return score;
    }
    public void GetWave()
    {

    }
    //return cash value.
    public int GetCash()
    {
        return cash;
    }

    //request permission to spend cash.
    public bool SpendCash(int value)
    {
        if((cash - value) >= 0)
        {
            cash -= value;
            return true;
        }
        return false;
    }

    public void AddResource(int rec)
    {
        cash = cash + rec;
    }

    public void AddScore(int rec)
    {
        score = score + rec;
    }
    private void GameOver()
    {
        
    }
}
