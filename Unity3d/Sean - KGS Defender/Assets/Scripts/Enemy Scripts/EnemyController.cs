using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;

    //Enemy Stats
    public float healthPoints;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float resourceValue;
    public float waveModifier;
    public float distToTarget;

    // Use this for initialization
    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    agent.SetDestination(target.position);
        agent.speed = moveSpeed;

        distToTarget = Vector3.Distance(target.transform.position, this.transform.position);

        if (distToTarget <= 20)
        {
            agent.speed = 0;
            animator.SetBool("ReachTarget", true);
        }
	}

}
