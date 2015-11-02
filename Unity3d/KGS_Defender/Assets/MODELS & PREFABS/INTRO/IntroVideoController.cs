using UnityEngine;
using System.Collections;

public class IntroVideoController : MonoBehaviour
{
    public bool monoOver;
    public bool enterWarp;
    public bool exitWarp;

    public GameObject warpPS;
    public GameObject warpSpeedFX;
    public GameObject introShip;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(monoOver == true && enterWarp == false)
        {
            StartCoroutine(ShipIntro());

            monoOver = false;
            enterWarp = true;             
        }
        if(enterWarp = true && exitWarp == true)
        {
            StartCoroutine(EnterWarp());

            enterWarp = false;
            exitWarp = false;
        }
	}

    IEnumerator ShipIntro()
    {
        warpPS.SetActive(true);
        yield return new WaitForSeconds(2.75f);
        warpPS.GetComponent<ParticleSystem>().Play(true);
        yield return new WaitForSeconds(.25f);
        introShip.SetActive(false);
                
        exitWarp = true;
    }
    IEnumerator EnterWarp()
    {
        yield return new WaitForSeconds(1.80f);
        warpSpeedFX.SetActive(true);

        gameObject.GetComponentInParent<AudioSource>().Stop();
    }
}
