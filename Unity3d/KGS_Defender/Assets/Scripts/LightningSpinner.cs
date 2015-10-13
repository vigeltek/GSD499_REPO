using UnityEngine;
using System.Collections;

public class LightningSpinner : MonoBehaviour {

    //public Vector3 direction;
    public float rotSpeed;
    public GameObject spinner;
 

	// Use this for initialization
	void Start () {
	
        
	}
	


	// Update is called once per frame
	void Update () {
        spinner.transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
    }





}
