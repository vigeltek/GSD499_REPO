using UnityEngine;
using System.Collections;

public class TurretUpgrade : MonoBehaviour {

    public int currentLevel= 1;
    public int MaxUpgrade;

    public int upgradeCost;

    public int L2UpgradeCost;
    public float L2fireRate;
    public int L2dmg;

    public float L3fireRate;
    public int L3dmg;

    public int CostOfUpgrade()
    {
        int temp = 0;

        switch (currentLevel)
        {
            case 1:
                temp = upgradeCost;
                break;

            case 2:
                temp = L2UpgradeCost;
                break;
            case 3:
                temp = 0;
                break;
        }

        return temp;
    }

    public void Upgrade()
    {
       switch(currentLevel)
        {
            case 1:
                currentLevel = 2;
                this.gameObject.GetComponent<WeaponController>().FireSpeed = L2fireRate;
                this.gameObject.GetComponent<WeaponController>().LDmg = L2dmg;
                break;

            case 2:
                currentLevel = 3;
                this.gameObject.GetComponent<WeaponController>().FireSpeed = L3fireRate;
                this.gameObject.GetComponent<WeaponController>().LDmg = L3dmg;
                break;
        }

        

    }
}
