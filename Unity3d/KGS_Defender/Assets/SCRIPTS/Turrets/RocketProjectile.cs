using UnityEngine;
using System.Collections;

public class RocketProjectile : MonoBehaviour {

    public float trackingLockDelay;
    public Vector3 movementSpeed;
    public float damage;
    Rigidbody rb;
    public GameObject RocketHit;
    public bool locked = false;
    public Vector3 offset;

    public GameObject target;
    public laserTurretHeadPoint hp;
    public float rotationalMod;

    private Vector3 targetPoint;
    private Quaternion targetRotation;

    public GameObject Parent;

    // Use this for initialization
    void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine("Wait");
    }

    void FixedUpdate()
    {
        transform.Translate(movementSpeed);
        // rb.AddRelativeForce(movementSpeed * 2);
    }


    // Update is called once per frame
    void Update () {

        if (target != null)
        {
                targetPoint = target.transform.position - transform.position;
            targetPoint += offset;
                targetRotation = Quaternion.LookRotation(targetPoint, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationalMod);
            
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
            if (c.gameObject.tag != "Player")
            {
                Destroy(this.gameObject);
            }

        }
    }

    void ApplyDamage(GameObject go)
    {
        Stats stat = go.GetComponent<Stats>();
        stat.DamageObject(damage, Parent);
    }

    public IEnumerable Wait()
    {
        yield return new WaitForSeconds(trackingLockDelay);
        locked = true;
    }
}
