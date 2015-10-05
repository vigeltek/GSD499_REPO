using UnityEngine;
using System.Collections;

public class RocketProjectile : MonoBehaviour {


    public Vector3 movementSpeed;
    public float damage;
    Rigidbody rb;
    public GameObject RocketHit;


    public GameObject target;
    public laserTurretHeadPoint hp;
    public float rotationalMod;

    private Vector3 targetPoint;
    private Quaternion targetRotation;

    // Use this for initialization
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        rb.AddRelativeForce(movementSpeed * 2);
    }


    // Update is called once per frame
    void Update () {

        if (target != null)
        {
                targetPoint = target.transform.position - transform.position;
                targetRotation = Quaternion.LookRotation(targetPoint, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationalMod);
            
        }


    } //end update

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Enemy"))
        {
            ContactPoint[] hitpoint = c.contacts;
            Instantiate(RocketHit, hitpoint[0].point, c.transform.rotation);
            ApplyDamage(c.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void ApplyDamage(GameObject go)
    {
        Stats stat = go.GetComponent<Stats>();
        stat.DamageObject(damage);
    }
}
