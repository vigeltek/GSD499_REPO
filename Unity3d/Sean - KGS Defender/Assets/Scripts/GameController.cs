using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public GameObject spider;
    public GameObject buzzer;
    public GameObject tank;
    public GameObject mastermind;

	public Transform[] spawnPoints;
	public Transform[] targets;

    public float spawnTimer = 2.5f;

    // Enemy stats to be applied to spawned enemy
    public float healthPoints;
	public float attackPower;
	public float attackSpeed;
	public float moveSpeed;
	public float resourceValue;

	private float waveModifier;
	private int wave;
	private int totalWaves;
	private int enemyCount;
	private int numToSpawn;
    private int enemiesRemaining;
    private bool bossWave;
    private GameObject[] currentEnemies;

    void Awake()
    {
        Screen.SetResolution(1074, 768, true);
        wave = 0;
        waveModifier = 0;
        enemyCount = 0;
        numToSpawn = 0;
        enemiesRemaining = 0;       
    }

    // Use this for initialization
    void Start()
	{  

    }
	
	// Update is called once per frame
	void Update () 
	{
        if (enemiesRemaining == 0)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        wave++;
        numToSpawn = 2 * wave;
        if (wave > 1)
        {
            waveModifier = 0.1f;
            waveModifier = waveModifier * (wave-1);
        }

        WaveGenerator(wave, numToSpawn);
    }

    void WaveGenerator(int waveCount, int toSpawn)
    {
        currentEnemies = new GameObject[toSpawn];
        enemyCount = 0;
        enemiesRemaining = toSpawn;

        switch (waveCount)
        {
            case 1:
            case 2:
                StartCoroutine(SpiderSpawner(toSpawn));
                bossWave = false;
                break;

            case 3:
                StartCoroutine(SpiderSpawner(toSpawn));
                bossWave = true;
                break;

            case 4:
            case 5:
                StartCoroutine(BuzzerSpawner(toSpawn / 2));
                StartCoroutine(SpiderSpawner(toSpawn / 2));
                bossWave = false;
                break;

            case 6:
                StartCoroutine(BuzzerSpawner(toSpawn / 2));
                StartCoroutine(SpiderSpawner(toSpawn / 2));
                bossWave = true;
                break;
        }
    }

    public void RemoveEnemy()
    {
        enemiesRemaining--;
    }

    IEnumerator SpiderSpawner(int spawnNum)
    {
        while (enemyCount < spawnNum)
        {
            yield return new WaitForSeconds(spawnTimer);
            int index = Random.Range(0, spawnPoints.Length);
            GameObject clone;

            // If it is a boss wave, spawn a larger more powerful version.
            if ((bossWave == true) && (enemyCount == spawnNum - 1))
            {
                // Instantiate the enemy of the appropriate type.
                clone = (GameObject)Instantiate(spider, (spawnPoints[index].position), spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                clone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                clone.GetComponent<EnemyController>().healthPoints = healthPoints * 5;
                clone.GetComponent<EnemyController>().attackPower = attackPower * 2;
                clone.GetComponent<EnemyController>().attackSpeed = attackSpeed * 2;
                clone.GetComponent<EnemyController>().moveSpeed = moveSpeed;
                clone.GetComponent<EnemyController>().resourceValue = resourceValue * 10;

                clone.transform.localScale += new Vector3(1, 1, 1);

                // Add to the currentEnemies array.
                currentEnemies[enemyCount] = clone;

                // Increase enemy count for tracking purposes.
                enemyCount++;
            }
            else
            {
                // Instantiate the enemy of the appropriate type.
                clone = (GameObject)Instantiate(spider, spawnPoints[index].position, spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                clone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                clone.GetComponent<EnemyController>().healthPoints = healthPoints + (healthPoints * waveModifier);
                clone.GetComponent<EnemyController>().attackPower = attackPower + (attackPower * waveModifier);
                clone.GetComponent<EnemyController>().attackSpeed = attackSpeed + (attackSpeed * waveModifier);
                clone.GetComponent<EnemyController>().moveSpeed = moveSpeed;
                clone.GetComponent<EnemyController>().resourceValue = resourceValue;

                // Add to the currentEnemies array.
                currentEnemies[enemyCount] = clone;

                // Increase enemy count for tracking purposes.
                enemyCount++;
            }
        }
    }

    IEnumerator BuzzerSpawner(int spawnNum)
    {
        while (enemyCount < spawnNum)
        {
            yield return new WaitForSeconds(spawnTimer + 0.25f);
            int index = Random.Range(0, spawnPoints.Length);
            GameObject clone;

            // If it is a boss wave, spawn a larger more powerful version.
            if ((bossWave == true) && (enemyCount == spawnNum - 1))
            {
                // Instantiate the enemy of the appropriate type.
                clone = (GameObject)Instantiate(buzzer, (spawnPoints[index].position), spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                clone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                clone.GetComponent<EnemyController>().healthPoints = (healthPoints * 1.5f) * 5;
                clone.GetComponent<EnemyController>().attackPower = (attackPower * 0.5f) * 2;
                clone.GetComponent<EnemyController>().attackSpeed = (attackSpeed * 2f) *2;
                clone.GetComponent<EnemyController>().moveSpeed = moveSpeed - 10;
                clone.GetComponent<EnemyController>().resourceValue = (resourceValue * 2) *10;

                clone.transform.localScale += new Vector3(1, 1, 1);

                // Add to the currentEnemies array.
                currentEnemies[enemyCount] = clone;

                // Increase enemy count for tracking purposes.
                enemyCount++;
            }
            else
            {
                // Instantiate the enemy of the appropriate type.
                clone = (GameObject)Instantiate(buzzer, spawnPoints[index].position, spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                clone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                clone.GetComponent<EnemyController>().healthPoints = (healthPoints * 1.5f) + ((healthPoints * 1.5f) * waveModifier);
                clone.GetComponent<EnemyController>().attackPower = (attackPower * 0.5f) + ((attackPower * 0.5f) * waveModifier);
                clone.GetComponent<EnemyController>().attackSpeed = (attackSpeed * 2f) + ((attackSpeed * 2f) * waveModifier);
                clone.GetComponent<EnemyController>().moveSpeed = moveSpeed - 10;
                clone.GetComponent<EnemyController>().resourceValue = resourceValue * 2;

                // Add to the currentEnemies array.
                currentEnemies[enemyCount] = clone;

                // Increase enemy count for tracking purposes.
                enemyCount++;
            }
        }
    }

}
