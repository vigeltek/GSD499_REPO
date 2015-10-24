using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    private NavMeshAgent agent;
    private Animator animator;
    public GameObject GC;
    public GameObject attackHit;
    private bool attackObject;
    private bool canFire;
    public GameObject collObject;
    private Transform destination;
    private AudioSource AS;

    //Enemy Stats
    public float healthPoints;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float resourceValue;

    void Awake ()
    {
        Physics.IgnoreLayerCollision(8, 9);
        Physics.IgnoreLayerCollision(9, 9);
        Physics.IgnoreLayerCollision(9, 10);

        AS = gameObject.GetComponent<AudioSource>();
        GC = GameObject.FindGameObjectWithTag("GameController");
        destination = GC.GetComponent<GameController>().destination;
    }

    // Use this for initialization
    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        attackObject = false;
        canFire = false;

        agent.speed = moveSpeed;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (collObject != null)
        {
            if (canFire == true && attackObject == true && collObject.GetComponent<Stats>().health > 0)
            {
                AttackTarget(collObject);
            }
        }
        if (collObject == null)
        {
            agent.SetDestination(destination.transform.position);
            animator.SetBool("ReachTarget", false);
            agent.Resume();
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

            collTarget.GetComponent<Stats>().DamageObject(attackPower, gameObject);
            AS.Play();
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
