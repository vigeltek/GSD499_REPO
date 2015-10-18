using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour {

    public float LDmg; //variable for deliverying lightning damage /30
    public float laserLineReset; //Rests line renderer
    public Material laserMat; //Material to use for laser lineRenderer;

    //Determines type of weapon this script is controlling
    public bool isLaser;
    public bool isRocket;
    public bool isLightning;

    //handles how frequently weapon can fire, and firing control.
    public float FireSpeed;
    public bool canFire;

    //Objects for weapon projectiles, and effects.
    public GameObject LaserPrefab;
    public GameObject RocketPrefab;
    public LightningBolt bolt;
    public GameObject lightningObject;

    //location of firePoint for laser (FirePoint1) and rockets (all three)
    public GameObject FirePoint1;
    public GameObject FirePoint2;
    public GameObject FirePoint3;
    //Target passed to weapons for homing, and locking in.
    public GameObject Target;

    //turret look for swivel on laser, and rockets.  Laser head point for laser vertical tracking.
    public TurretLook swivel1;
    public laserTurretHeadPoint swivel2;

    //list of targets to allow tracking the order enemies enter firing area.
    public ArrayList targetList;

	// Use this for initialization
	void Start () {
        targetList = new ArrayList();

        if(isLaser)
        {
            FirePoint1.AddComponent<LineRenderer>();
            LineRenderer laserRender = FirePoint1.GetComponent<LineRenderer>();
            laserRender.enabled = false;
            laserRender.SetPosition(0, FirePoint1.transform.position);
            laserRender.SetPosition(1, FirePoint1.transform.position);
            laserRender.material = laserMat;
            

        }
	}
	
	// Update is called once per frame
	void Update () {

        for(int i = targetList.Count -1; i >= 0; i--)
        {
            GameObject temp = (GameObject)targetList[i];
            if(temp == null)
            {
                targetList.RemoveAt(i);
            }
            
        }

        if(Target == null)
        {
            try
            {
                Target = (GameObject)targetList[0];
            }
            catch { }
        }
        //if the weapon can fire, fire.
       if(canFire)
        {
            Fire();
        }
        

	}


    private void Fire()
    {
        if (canFire) //additional check to avoid issues with coroutine for canFire.
        {
            //Code for firing lasers
            if (isLaser)
            {
                //Avoid invalid reference error, if tower attempts to fire without a target.
                try
                {
                    //Make sure a target is available
                    Target = (GameObject)targetList[0];
                    //lock onto the target
                    swivel1.target = Target;
                    swivel2.target = Target;
                    canFire = false; //keep tower from firing repeatedly.

                    StartCoroutine("Wait"); //allows tower time to rotate before firing.
                    
                }
                catch
                {
                    //No handling currently, possible future use for debugging.
                }
                
            }

            //code for firing rockets
            if (isRocket && Target != null)
            {
                //Avoid invalid reference error, if tower attempts to fire without a target.
                try
                {
                    //Make sure target is valid, and set target.
                    Target = (GameObject)targetList[0];
                    swivel1.target = Target;
                    StartCoroutine("Wait"); //gives rocket launcher time to target.
                    canFire = false;
                }
                catch
                {
                    //possible future debug use.
                }

            }

            //Code for firing lightning
            if (isLightning)
            {
                //Avoid invalid reference error, if tower attempts to fire without a target.
                try
                {
                    //Validate target, set target.
                    GameObject targetGO = (GameObject)targetList[0];
                    //Set target for the lightning bolt effect.
                    bolt.target = targetGO.transform;
                    //Turn on the lighting bolt object.
                    lightningObject.SetActive(true);

                    //Apply damage for lightning ove time. (assumed 30FPS) as target is web build.
                    //This makes damage per second in a constant flow.
                     Stats stat = targetGO.GetComponent<Stats>();
                        stat.DamageObject(LDmg/30, this.gameObject); 
                }
                catch
                {
                    //If no target is available, turn off targets, and turn off lightning bolt effect.
                    Target = null;
                    bolt.target = null;
                    lightningObject.SetActive(false); 
                }
            }

            if (!canFire)
            {
                //set canFire = true after proper wait time.
                StartCoroutine("ResetFire");
            }
        }

    }

    //Allows turret time to focus before firing.
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        if (isLaser)
        {
            //  GameObject l1 = (GameObject)Instantiate(LaserPrefab, FirePoint1.transform.position, FirePoint1.transform.rotation);
            //  l1.GetComponent<LaserProjectile>().parent = this.gameObject;
            //  l1.GetComponent<LaserProjectile>().damage = LDmg;
            FireLaser();
        }
        if(isRocket && Target != null)
        {
            FireRockets();
        }
    
    }

    //Resets canFire after delay
    IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(FireSpeed);
        canFire = true;
    }

    IEnumerator ResetLineRenderer()
    {
        yield return new WaitForSeconds(laserLineReset);
        FirePoint1.GetComponent<LineRenderer>().SetPosition(1, FirePoint1.transform.position);
        FirePoint1.GetComponent<LineRenderer>().enabled = false;


    }

    void OnTriggerEnter(Collider c)
    {
        bool isFound = false;

        if(c.gameObject.CompareTag("Enemy"))
        {
            //Add enemy to the target list, and set active target.
            targetList.Add(c.gameObject);
            //Debug.Log(c.gameObject.name + " has been added to the target list");

            Target = (GameObject)targetList[0];

            for (int i = 0; i < targetList.Count; i++)
            {
                GameObject temp = (GameObject)targetList[i];
                if (temp = c.gameObject)
                {
                    isFound = true;
                    break;
                }

            }

            if (!isFound)
            {
                targetList.Add(c.gameObject);
                Target = (GameObject)targetList[0];
                Debug.Log("Added Enemy");
            }
        }
    }
   
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            //Remove enemy from target list, and clear last damage as callback for targeting is no longer needed.
            c.gameObject.GetComponent<Stats>().ClearLastWeapon();
            targetList.Remove(c.gameObject);
            //Debug.Log(c.gameObject.name + " has been Removed from the target list");
            resetTarget(); //Removes current target to be set in update by next frame, we no longer want to target enemies out of range.
        }
    }
    void resetTarget()
    {
        //Rests all targeting scripts in preparation for getting next target.
        GameObject tempTarget = null;
        try
        {
            tempTarget = (GameObject)targetList[0];
        
        if (tempTarget != null) { Target = tempTarget; }

        if (isLaser || isRocket)
        {
            swivel1.target = Target;
        }
        if (isLaser)
        {
            swivel2.target = Target;
            FirePoint1.GetComponent<LineRenderer>().SetPosition(1, FirePoint1.transform.position);
        }
        if (isLightning)
        {
            if (Target != null)
            {
                bolt.target = Target.transform;
            }
        }
        }
        catch { Target = null; }
    }

    //Confirms that the current target has been destroyed, removes it from the list, and rests targeting system.
    public void DeathConfirmation(GameObject go)
    {
        try
        {
            targetList.Remove(go);
        }
        catch
        { }
        
        
        resetTarget();
    }

    void FireLaser()
    {
        try
        {
            Target = (GameObject)targetList[0];
                FirePoint1.GetComponent<LineRenderer>().SetPosition(1, Target.transform.position);
                FirePoint1.GetComponent<LineRenderer>().enabled = true;
            Target.GetComponent<Stats>().DamageObject(LDmg, this.gameObject);

                StartCoroutine("ResetLineRenderer");
        }
        catch { resetTarget(); }
    }
    //actualy fires rockets from coroutine wait if isRocket
    void FireRockets()
    {
        try
        {
            Target = (GameObject)targetList[0];

            //Spawn three rocket objects.
            GameObject rocket1 = (GameObject)Instantiate(RocketPrefab, FirePoint1.transform.position, FirePoint1.transform.rotation);
            GameObject rocket2 = (GameObject)Instantiate(RocketPrefab, FirePoint2.transform.position, FirePoint2.transform.rotation);
            GameObject rocket3 = (GameObject)Instantiate(RocketPrefab, FirePoint3.transform.position, FirePoint3.transform.rotation);

            //Set the target for rocket homing scritps.
            rocket1.GetComponent<RocketProjectile>().target = Target;
            rocket2.GetComponent<RocketProjectile>().target = Target;
            rocket3.GetComponent<RocketProjectile>().target = Target;
            //Set the rockets parent to weaponCrontroller for return calls from dead objects.
            rocket1.GetComponent<RocketProjectile>().Parent = this.gameObject;
            rocket2.GetComponent<RocketProjectile>().Parent = this.gameObject;
            rocket3.GetComponent<RocketProjectile>().Parent = this.gameObject;
            //Set the damage on rockets
            rocket1.GetComponent<RocketProjectile>().damage = LDmg;
            rocket2.GetComponent<RocketProjectile>().damage = LDmg;
            rocket3.GetComponent<RocketProjectile>().damage = LDmg;
        }
        catch { resetTarget(); }
    }
}
