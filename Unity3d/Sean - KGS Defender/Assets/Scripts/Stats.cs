using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health;
    WeaponController wc;
    GameObject LastWeapon;
    public GameObject DestructionParticles;
    private GameObject spawnController;

    // Use this for initialization
    void Start ()
    {
        spawnController = GameObject.FindGameObjectWithTag("Spawn Manager");
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        if(health <= 0)
        {
            if (LastWeapon != null)
            {
                wc = LastWeapon.GetComponent<WeaponController>();
                wc.DeathConfirmation(this.gameObject);
            }
            //Instantiate death explosion
            Instantiate(DestructionParticles, this.gameObject.transform.position, this.gameObject.transform.rotation);
            spawnController.GetComponent<SpawnController>().RemoveEnemy();
            //Finally destroy this object.
            Destroy(this.gameObject);
        }

	}

    public void DamageObject(float dmg, GameObject parent)
    {    
        LastWeapon = parent;
        health -= dmg;
    }

    public void ClearLastWeapon()
    {
        LastWeapon = null;
    }
}
