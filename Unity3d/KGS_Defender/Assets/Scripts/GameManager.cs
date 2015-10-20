using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float cash;
    public float score;
    public GameObject wave;
    private string txt;

    public GameObject ship;
    public GameObject shield;
    private bool gameOver = false;
    private float shipHealth;
    private float shieldHealth;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        float shipHealth = ship.GetComponent<Stats>().health;
        float shieldHealth = shield.GetComponent<Stats>().health;

        if (shieldHealth <= 0 && shipHealth <= 0)
        {
            gameOver = true;
            UIGameOverMenu.Show();
        }
    }
    public float GetScore()
    {
        return score;
    }

    //return cash value.
    public float GetCash()
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

    public void AddResource(float rec)
    {
        cash = cash + rec;
    }

    public void AddScore(float rec)
    {
        score = score + rec;
    }
}
