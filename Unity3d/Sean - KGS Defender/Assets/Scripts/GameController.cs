using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject spider;
    public GameObject buzzer;
    public GameObject tank;
    public GameObject mastermind;
	public float spawnTimer = 3f;
	public Transform[] spawnPoints;
	public Transform[] targets;

	// Enemy stats to be applied to spawned enemy
	public float healthPoints;
	public float attackPower;
	public float attackSpeed;
	public float moveSpeed;
	public float resourceValue;
	public float waveModifier;

	private int wave;
	private int totalWaves;
	private int enemyCount;
	private int enemyTarget;
    public GameObject[] currentEnemies;

    // Use this for initialization
    void Start()
	{  
        wave = 1;
		enemyCount = 0;
		enemyTarget = 5 * wave;
        currentEnemies = new GameObject[enemyTarget];
        StartCoroutine(SpiderSpawner(enemyTarget));
    }
	
	// Update is called once per frame
	void Update () 
	{

	}

    void Awake()
    {
        Screen.SetResolution(1074, 768, true);
        
    }

	IEnumerator SpiderSpawner(int enemyTarget)
	{
		while (enemyCount < enemyTarget)
		{
            yield return new WaitForSeconds(spawnTimer);
            int index = Random.Range(0, spawnPoints.Length);

            // Create empty game object to hold each enemy
            GameObject clone;

            // Instantiate the enemy of the appropriate type.
            clone = (GameObject)Instantiate(spider, spawnPoints[index].position, spawnPoints[index].rotation);

            // Assign target to enemy according to which spawnpoint they spawn from.
            clone.GetComponent<EnemyController>().target = targets[index];

            // Assign enemy stats
            clone.GetComponent<EnemyController>().healthPoints = healthPoints;
            clone.GetComponent<EnemyController>().attackPower = attackPower;
            clone.GetComponent<EnemyController>().attackSpeed = attackSpeed;
            clone.GetComponent<EnemyController>().moveSpeed = moveSpeed;
            clone.GetComponent<EnemyController>().resourceValue = resourceValue;
            clone.GetComponent<EnemyController>().waveModifier = waveModifier;

            currentEnemies[enemyCount] = clone;

            enemyCount++;
		}
	}
}
