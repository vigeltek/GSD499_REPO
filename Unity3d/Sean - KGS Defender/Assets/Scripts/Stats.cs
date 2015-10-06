using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health;
    WeaponController wc;
    GameObject LastWeapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(health <= 0)
        {
            if (LastWeapon != null)
            {
                wc = LastWeapon.GetComponent<WeaponController>();
                wc.DeathConfirmation(this.gameObject);
            }
            //Instantiate death explosion

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
