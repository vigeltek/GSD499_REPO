using UnityEngine;
using System.Collections;

public class laserTurretHeadPoint : MonoBehaviour {

    public GameObject target;
    public float VerticalOffset = 0;
    public float RotationalSpeedModifyer;
    public GameObject fireOrigin;
    public Vector3 direction;

    private Vector3 targetPoint;
    private Quaternion targetRotation;
    RaycastHit hit;

    public bool canRotate;

    // Use this for initialization
    void Start()
    {
       // canRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate)
        {
            Rotation();
        }

    }

    public void TriggerRotation()
    {
        canRotate = true;
    }
    private void Rotation()
    {
        if (target != null)
        {
             targetPoint = new Vector3(target.transform.position.x,
                 target.transform.position.y + VerticalOffset, target.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationalSpeedModifyer * Time.deltaTime);
        }
    }

    }
