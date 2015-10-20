using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health;
    public bool isFriendly;
    WeaponController wc;
    GameObject LastWeapon;
    public GameObject DestructionParticles;
    public GameObject turPanels;
    public GameObject gameController;
    public float recValue;
   // public GameObject TPM;
    public ArrayList towerList;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        turPanels = GameObject.FindGameObjectWithTag("PanelPlacement");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void RegisterTower(GameObject t)
    {
        bool isFound = false;
        for (int i = 0; i < towerList.Count; i++)
        {
            GameObject g = (GameObject)towerList[i];
            if (t = g)
            {
                isFound = true;
                break;
            }
        }
        if (!isFound)
        {
            towerList.Add(t);
        }

    }

    public void RemoveTower(GameObject t)
    {
        towerList.Remove(t);
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
        gameController.GetComponent<SpawnController>().RemoveEnemy(recValue);

        // Instantiate death explosion
        Instantiate(DestructionParticles, this.gameObject.transform.position, this.gameObject.transform.rotation);

        // Finally destroy this object.
        gameObject.GetComponent<EnemyController>().DestroySelf();
    }
}
