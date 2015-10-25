using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour
{
    private float shipHealth;
    private float startHealth;
    public GameObject[] flames;
    public GameObject explosion;

    void Start ()
    {
        startHealth = gameObject.GetComponent<Stats>().health;
    }
	
	// Update is called once per frame
	void Update ()
    {
        shipHealth = gameObject.GetComponent<Stats>().health;

        StartFlames(shipHealth / startHealth);

        if (shipHealth <= 0)
        {
            GameObject boomFX = (GameObject)Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
            boomFX.GetComponent<AudioSource>().Stop();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void StartFlames(float healthPrecent)
    {
        if(healthPrecent <= .75f)
        {
            flames[0].SetActive(true);
        }
        if(healthPrecent <= .50f)
        {
            flames[1].SetActive(true);
        }
        if(healthPrecent <= .25f)
        {
            flames[2].SetActive(true);
        }
    }
}
