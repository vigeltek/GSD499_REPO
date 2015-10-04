using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

	}

    public void DamageObject(float dmg)
    {
        health -= dmg;
    }
}
