using UnityEngine;
using System.Collections;

public class TurretLook : MonoBehaviour {

    public bool isLookX;
    public bool isLookY;
    public GameObject target;
    public laserTurretHeadPoint hp;
    public float rotationalMod;

    private Vector3 targetPoint;
    private Quaternion targetRotation;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        if (target != null)
        {
            //Move on XAxis
            if (isLookX && !isLookY)
            {
                targetPoint = new Vector3(target.transform.position.x,
                    transform.position.y, target.transform.position.z) - transform.position;
            }

            //Move on YAxis
            if (isLookY && !isLookX)
            {
                targetPoint = new Vector3(transform.position.x,
                    transform.position.y, transform.position.z) - transform.position;
            }
            if (isLookX && isLookY)
            {
                Debug.Log("Please select only one Axis on Turret Look");
            }
            if (isLookX || isLookY)
            {

                targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
        
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationalMod);
            }
        }
        
    }
}
