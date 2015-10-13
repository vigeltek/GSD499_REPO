using UnityEngine;
using System.Collections;

public class LaserProjectile : MonoBehaviour {

    public Vector3 movementSpeed;
    public float damage;
    Rigidbody rb;
    public GameObject parent;

    public GameObject LaserHit;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
	}


    void FixedUpdate()
    {
        
      // rb.AddRelativeForce(movementSpeed*2);
        transform.Translate(movementSpeed);
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.CompareTag("Enemy"))
        {
            ContactPoint[] hitpoint = c.contacts;
            Instantiate(LaserHit, hitpoint[0].point, c.transform.rotation);
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
        stat.DamageObject(damage, parent);
    }

}
