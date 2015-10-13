using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {
    public delegate void AddNewTurretHandler(UnitTurret tower);
    public static event AddNewTurretHandler onAddNewTowerE;      //add new tower in runtime
                                                                // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
