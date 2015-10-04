using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public GameObject enemyType;
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
	private GameObject[] currentEnemies;

	// Use this for initialization
	void Start()
	{
		wave = 1;
		enemyCount = 0;
		enemyTarget = 5 * wave;

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

    void Awake()
    {
        Screen.SetResolution(1074, 768, true);
        StartCoroutine(Spawner(enemyTarget));
    }

	IEnumerator Spawner(int enemyTarget)
	{
		while (enemyCount < enemyTarget)
		{
            yield return new WaitForSeconds(3f);
            int index = Random.Range(0, spawnPoints.Length);
            GameObject clone = (GameObject)Instantiate(enemyType, spawnPoints[index].position, spawnPoints[index].rotation);
            clone.GetComponent<MoveToTarget>().target = targets[index];
            clone.GetComponent<NavMeshAgent>().speed = moveSpeed;

            //currentEnemies[enemyCount] = clone;

			enemyCount++;
		}
	}

    //void SpawnEnemy()
    //{
    //    int index = Random.Range(0, 2);
    //    GameObject clone = (GameObject)Instantiate(enemyType, spawnPoints[index].position, spawnPoints[index].rotation);
    //    clone.GetComponent<MoveToTarget>().target = targets[index];
    //    clone.GetComponent<NavMeshAgent>().speed = moveSpeed;

    //    currentEnemies[enemyCount] = clone;
    //}
}
