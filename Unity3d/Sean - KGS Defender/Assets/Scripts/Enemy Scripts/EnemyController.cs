﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject gameController;

    //Enemy Stats
    public float healthPoints;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float resourceValue;

    public float distToTarget;

    private float currentHealth;

    // Use this for initialization
    void Awake () 
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

        IsDead();
	}

    void IsDead()
    {
        if (gameObject.GetComponent<Animator>().GetBool("ReachTarget") == true)
        {

            gameController.GetComponent<GameController>().RemoveEnemy();
            Destroy(gameObject);

        }
    }

}
