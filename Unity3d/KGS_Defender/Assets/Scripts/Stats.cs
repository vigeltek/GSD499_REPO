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

	}

    public void DamageObject(float dmg, GameObject parent)
    {    
        LastWeapon = parent;
        health -= dmg;

        if(health <= 0 && LastWeapon != null && !isFriendly)
        {
            DestroyEnemy();
        }
    }

    public void ClearLastWeapon()
    {
        LastWeapon = null;
    }

    public void DestroyEnemy()
    {
        turPanels.GetComponentInChildren<WeaponController>().DeathConfirmation(this.gameObject);
        //turPanels.BroadcastMessage("DeathConfirmation", this.gameObject, SendMessageOptions.DontRequireReceiver);
        spawnController.GetComponent<SpawnController>().RemoveEnemy(recValue);

        // Instantiate death explosion
        Instantiate(DestructionParticles, this.gameObject.transform.position, this.gameObject.transform.rotation);

        // Finally destroy this object.
        gameObject.GetComponent<EnemyController>().DestroySelf();
    }
}
