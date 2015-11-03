using UnityEngine;
using System.Collections;

public class IntroVideoController : MonoBehaviour
{
    public bool monoOver;
    public bool enterWarp;
    public bool exitWarp;
    private bool reachedPlanet = false;

    public GameObject warpPS;
    public GameObject warpSpeedFX;
    public GameObject warpPS2;
    public GameObject introShip;
    public GameObject announcement;
    public GameObject warning;
    public GameObject alienSystem;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GameObject.FindGameObjectWithTag("IntroShip").GetComponent<Animator>();
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
        if(enterWarp == true && exitWarp == true)
        {
            StartCoroutine(EnterWarp());

            enterWarp = false;
            exitWarp = false;
        }
        if(enterWarp == false && exitWarp == true)
        {
            StartCoroutine(ExitWarp());

        }
        if (reachedPlanet == true) 
        {

            warpPS2.GetComponent<ParticleSystem>().Play(true);
            announcement.SetActive(false);
            alienSystem.SetActive(true);
            warning.SetActive(true);
            anim.SetBool("HitPlanet", true);
        }

	}

    IEnumerator ShipIntro()
    {
        anim.SetBool("IsStart", true);
        yield return new WaitForSeconds(1.75f);
        warpPS.SetActive(true);
        warpPS.GetComponent<ParticleSystem>().Play(true);

        exitWarp = true;
    }
    IEnumerator EnterWarp()
    { 
        yield return new WaitForSeconds(2.50f);
        warpSpeedFX.SetActive(true);
        anim.SetBool("InWarp", true);

        gameObject.GetComponentInParent<AudioSource>().Stop();
        exitWarp = true;
    }
    IEnumerator ExitWarp()
    {
        announcement.SetActive(true);
        yield return new WaitForSeconds(10.5f);
        warpSpeedFX.SetActive(false);
        warpPS2.SetActive(true);

        reachedPlanet = true;
    }

}
