using UnityEngine;
using System.Collections;

public class ShieldDown : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
	    if(gameObject.GetComponent<Stats>().health <= 0)
        {
            // gameObject.GetComponent<Animator>().SetBool("Shield Down", true);
            Destroy(gameObject, 0.5f);        
        }
	}
}
