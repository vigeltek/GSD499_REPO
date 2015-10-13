using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    public Transform shield;
    public Transform ship;
    private bool shieldDown;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject spawnController;
    public GameObject attackHit;
    private bool attackObject;
    private bool canFire;
    public GameObject collObject;

    //Enemy Stats
    public float healthPoints;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float resourceValue;

    // Use this for initialization
    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        spawnController = GameObject.FindGameObjectWithTag ( "Spawn Manager" );

        attackObject = false;
        canFire = false;
        shieldDown = false;

        agent.SetDestination(shield.position);

        agent.speed = moveSpeed;
    }
	
	// Update is called once per frame
	void Update () 
    {

        if(canFire == true && attackObject == true && collObject.GetComponent<Stats>().health > 0)
        {
            AttackTarget(collObject);
        }
    }

    void OnTriggerEnter(Collider c)
    {   
        if (c.gameObject.CompareTag("Base Component"))
        {
            collObject = c.gameObject;
            agent.Stop();
            animator.SetBool("ReachTarget", true);
            gameObject.transform.LookAt(collObject.transform.position);

            attackObject = true;

            StartCoroutine("ResetFire");
        }
    }

    // Update is called once per frame
    private void AttackTarget(GameObject collTarget)
    {

        if (canFire)
        {
            
            GameObject projClone = (GameObject)Instantiate(attackHit, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);

            projClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(0,0,25);

            Physics.IgnoreLayerCollision(8,9);
            Physics.IgnoreLayerCollision(9, 9);
            Physics.IgnoreLayerCollision(9,10);

            collTarget.GetComponent<Stats>().DamageObject(attackPower, gameObject);

            canFire = false;
        }

        if (!canFire)
        {
            //set canFire = true after proper wait time.
            StartCoroutine("ResetFire");
        }
    }

    IEnumerator ResetFire()
    {
        yield return new WaitForSeconds(attackSpeed);
        canFire = true;
    }

}
