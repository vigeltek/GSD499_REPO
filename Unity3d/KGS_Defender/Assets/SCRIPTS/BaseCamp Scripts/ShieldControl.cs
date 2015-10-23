using UnityEngine;
using System.Collections;

public class ShieldControl : MonoBehaviour
{
    public GameObject shieldGen;
    private bool shieldOff = false;

	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Transform>().Rotate(0,Time.deltaTime * 360,0);
	    if(gameObject.GetComponent<Stats>().health <= 0 && shieldOff == false)
        {
            shieldOff = true;
            
            shieldGen.GetComponent<GeneratorControl>().ShieldDown(true);
            gameObject.GetComponent<Animator>().SetBool("Shield Down", true);
            Destroy(gameObject, 0.5f);        
        }

	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy Attack"))
        {
           
        }
    }

    void OnTriggerExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.cyan;
    }
}
