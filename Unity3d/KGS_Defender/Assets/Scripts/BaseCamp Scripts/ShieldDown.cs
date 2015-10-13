using UnityEngine;
using System.Collections;

public class ShieldDown : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(gameObject.GetComponent<Stats>().health <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Shield Down", true);
            Destroy(gameObject, 0.5f);
            
        }
	}
}
