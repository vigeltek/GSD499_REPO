using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int cash;
    public int score;
    public int level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public int GetScore()
    {
        return score;
    }
    public int GetLevel()
    {
        return level;
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
}
