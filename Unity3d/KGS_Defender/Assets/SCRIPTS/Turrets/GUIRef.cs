using UnityEngine;
using System.Collections;

public class GUIRef : MonoBehaviour {
    public GameObject upgradeCluster;

    public placementPanel GUIReference;
   
    public void Confirm()
    {
        GUIReference.ConfirmUpgrade();
        GUIReference = null;
        upgradeCluster.SetActive(false);
    }
}
