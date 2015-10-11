using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject gameController;
    public GameObject deathExplosion;

    //Enemy Stats
    public float healthPoints;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float resourceValue;

    public float distToTarget;

    private float currentHealth;

    // Use this for initialization
    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag ( "Game Manager" );
        currentHealth = healthPoints;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    agent.SetDestination(target.position);
        agent.speed = moveSpeed;

        distToTarget = Vector3.Distance(target.transform.position, this.transform.position);

        if (distToTarget <= 20)
        {
            agent.Stop();
            animator.SetBool("ReachTarget", true);
            
        }

        if (currentHealth <= 0)
        {
            IsDead();
        }

        IsDead();
	}

    void TakeDamage(int dmgAmount)
    {
        currentHealth = currentHealth - dmgAmount;
    }

    void IsDead()
    {
        if (animator.GetBool("ReachTarget") == true)
        {
            gameController.GetComponent<GameController>().RemoveEnemy();
            Destroy(gameObject);
        }
    }

}
