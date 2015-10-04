using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public bool isLaser;
    public bool isRocket;
    public bool isLightning;

    public float FireSpeed;
    public bool canFire;

    public GameObject LaserPrefab;
    public GameObject RocketPrefab;
    public GameObject LightningPrefab;

    public GameObject FirePoint1;
    public GameObject FirePoint2;
    public GameObject FirePoint3;
    public GameObject Target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Temperary Testing Code.
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }


	}


    private void Fire()
    {
        if (canFire)
        {
            if (isLaser)
            {
                canFire = false;
                Instantiate(LaserPrefab, FirePoint1.transform.position, FirePoint1.transform.rotation);
            }

            if (isRocket)
            {
                canFire = false;
                GameObject rocket1 = (GameObject)  Instantiate(RocketPrefab, FirePoint1.transform.position, FirePoint1.transform.rotation);
                GameObject rocket2 = (GameObject)Instantiate(RocketPrefab, FirePoint2.transform.position, FirePoint2.transform.rotation);
                GameObject rocket3 = (GameObject)Instantiate(RocketPrefab, FirePoint3.transform.position, FirePoint3.transform.rotation);
                rocket1.GetComponent<RocketProjectile>().target = Target;
                rocket2.GetComponent<RocketProjectile>().target = Target;
                rocket3.GetComponent<RocketProjectile>().target = Target;

            }

            if (isLightning)
            {
                canFire = false;
            }
            StartCoroutine("ResetFire");
        }
    }

    IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(FireSpeed);
        canFire = true;
    }

}
