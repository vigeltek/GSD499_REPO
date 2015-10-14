using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health;
    public bool isFriendly;
    WeaponController wc;
    GameObject LastWeapon;
    public GameObject DestructionParticles;
    public GameObject turPanels;
    public GameObject spawnController;
    public int recValue;

    // Use this for initialization
    void Start ()
    {
        spawnController = GameObject.FindGameObjectWithTag("Spawn Manager");
        turPanels = GameObject.FindGameObjectWithTag("PanelPlacement");
    }
	
	// Update is called once per frame
	void Update ()
    {
	
        if(health <= 0)
        {
            if (LastWeapon != null)
            {
                if (!isFriendly)
                {
                    turPanels.BroadcastMessage("DeathConfirmation", this.gameObject, SendMessageOptions.DontRequireReceiver);
                    spawnController.GetComponent<SpawnController>().RemoveEnemy(recValue);
                }

            }
            //Instantiate death explosion
            Instantiate(DestructionParticles, this.gameObject.transform.position, this.gameObject.transform.rotation);
            
            //Finally destroy this object.
            Destroy(this.gameObject, 0.3f);
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
