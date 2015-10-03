using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	public GameObject enemyType;
    public float spawnTimer = 3f;
    public Transform[] spawnPoints;
    public Transform[] targets;

    public float healthPoints;
    public float attackPower;
    public float attackSpeed;
    public float moveSpeed;
    public float resourceValue;
    public float waveModifier;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject clone = (GameObject)Instantiate(enemyType, spawnPoints[index].position, spawnPoints[index].rotation);
        clone.GetComponent<MoveToTarget>().target = targets[index];
        clone.GetComponent<NavMeshAgent>().speed = moveSpeed;
    }
}
