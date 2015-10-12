using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
    // Prefabs for the Enemy types.
    public GameObject spider;
    public GameObject buzzer;
    public GameObject tank;
    public GameObject mastermind;

    // Where the enemies will spawn from and the corresponding target to that spawnpoint.
	public Transform[] spawnPoints;
	public Transform[] targets;

    public float spawnTimer;

    // Enemy stats to be applied to spawned enemy
    public float healthPoints;
	public float attackPower;
	public float attackSpeed;
	public float moveSpeed;
	public float resourceValue;

	private float waveModifier;
	private int wave;
	private int totalWaves;
    public int spiderCount;
    public int buzzerCount;
    public int tankCount;
    public int mastermindCount;
	private int numToSpawn;
    public int enemiesRemaining;
    private bool bossWave;
    public GameObject[] currentEnemies;

    void Awake()
    {
        Screen.SetResolution(1074, 768, true);
        wave = 0;
        waveModifier = 0;
        spiderCount = 0;
        buzzerCount = 0;
        tankCount = 0;
        mastermindCount = 0;
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
        spawnTimer = Random.Range(1.5f, 3.0f);

        if (enemiesRemaining == 0)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        wave++;
        numToSpawn = 3 * wave;

        waveModifier = .10f * (wave-1);

        WaveGenerator(wave, numToSpawn);
    }

    void WaveGenerator(int waveCount, int toSpawn)
    {
        currentEnemies = new GameObject[toSpawn];
        spiderCount = 0;
        buzzerCount = 0;
        tankCount = 0;
        mastermindCount = 0;
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

            case 7:
            case 8:
                StartCoroutine(TankSpawner(toSpawn / 3));
                StartCoroutine(BuzzerSpawner(toSpawn / 3));
                StartCoroutine(SpiderSpawner(toSpawn / 3));
                bossWave = false;
                break;

            case 9:
                StartCoroutine(TankSpawner(toSpawn / 3));
                StartCoroutine(BuzzerSpawner(toSpawn / 3));
                StartCoroutine(SpiderSpawner(toSpawn / 3));
                bossWave = true;
                break;

            case 10:
                StartCoroutine(MasterMindSpawner(toSpawn));
                bossWave = false;
                break;
        }
    }

    public void RemoveEnemy()
    {
        enemiesRemaining--;
    }

    IEnumerator SpiderSpawner(int spawnNum)
    {
        while (spiderCount < spawnNum)
        {
            yield return new WaitForSeconds(spawnTimer);
            int index = Random.Range(0, spawnPoints.Length);
            GameObject spiderClone;

            // If it is a boss wave, spawn a larger more powerful version.
            if ((bossWave == true) && (spiderCount == spawnNum - 1))
            {
                // Instantiate the enemy of the appropriate type.
                spiderClone = (GameObject)Instantiate(spider, (spawnPoints[index].position), spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                spiderClone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                spiderClone.GetComponent<EnemyController>().healthPoints = healthPoints * 5;
                spiderClone.GetComponent<Stats>().health = spiderClone.GetComponent<EnemyController>().healthPoints;
                spiderClone.GetComponent<EnemyController>().attackPower = attackPower * 2;
                spiderClone.GetComponent<EnemyController>().attackSpeed = attackSpeed * 2;
                spiderClone.GetComponent<EnemyController>().moveSpeed = moveSpeed;
                spiderClone.GetComponent<EnemyController>().resourceValue = resourceValue * 10;

                spiderClone.transform.localScale += new Vector3(1, 1, 1);

                // Add to the currentEnemies array.
                currentEnemies[spiderCount] = spiderClone;

                // Increase enemy count for tracking purposes.
                spiderCount++;
            }
            else
            {
                // Instantiate the enemy of the appropriate type.
                spiderClone = (GameObject)Instantiate(spider, spawnPoints[index].position, spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                spiderClone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                spiderClone.GetComponent<EnemyController>().healthPoints = healthPoints + (healthPoints * waveModifier);
                spiderClone.GetComponent<Stats>().health = spiderClone.GetComponent<EnemyController>().healthPoints;
                spiderClone.GetComponent<EnemyController>().attackPower = attackPower + (attackPower * waveModifier);
                spiderClone.GetComponent<EnemyController>().attackSpeed = attackSpeed + (attackSpeed * waveModifier);
                spiderClone.GetComponent<EnemyController>().moveSpeed = moveSpeed;
                spiderClone.GetComponent<EnemyController>().resourceValue = resourceValue;

                // Add to the currentEnemies array.
                currentEnemies[spiderCount] = spiderClone;

                // Increase enemy count for tracking purposes.
                spiderCount++;
            }
        }
    }

    IEnumerator BuzzerSpawner(int spawnNum)
    {
        while (buzzerCount < spawnNum)
        {
            yield return new WaitForSeconds(spawnTimer + 0.50f);
            int index = Random.Range(0, spawnPoints.Length);
            GameObject buzzerClone;

            // If it is a boss wave, spawn a larger more powerful version.
            if ((bossWave == true) && (buzzerCount == spawnNum - 1))
            {
                // Instantiate the enemy of the appropriate type.
                buzzerClone = (GameObject)Instantiate(buzzer, (spawnPoints[index].position), spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                buzzerClone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                buzzerClone.GetComponent<EnemyController>().healthPoints = (healthPoints * 1.5f) * 5;
                buzzerClone.GetComponent<EnemyController>().attackPower = (attackPower * 0.5f) * 2;
                buzzerClone.GetComponent<EnemyController>().attackSpeed = (attackSpeed * 2f) *2;
                buzzerClone.GetComponent<EnemyController>().moveSpeed = moveSpeed - 10;
                buzzerClone.GetComponent<EnemyController>().resourceValue = (resourceValue * 2) *10;

                buzzerClone.transform.localScale += new Vector3(1, 1, 1);

                // Add to the currentEnemies array.
                currentEnemies[buzzerCount + spawnNum] = buzzerClone;

                // Increase enemy count for tracking purposes.
                buzzerCount++;
            }
            else
            {
                // Instantiate the enemy of the appropriate type.
                buzzerClone = (GameObject)Instantiate(buzzer, spawnPoints[index].position, spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                buzzerClone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                buzzerClone.GetComponent<EnemyController>().healthPoints = (healthPoints * 1.5f) + ((healthPoints * 1.5f) * waveModifier);
                buzzerClone.GetComponent<EnemyController>().attackPower = (attackPower * 0.5f) + ((attackPower * 0.5f) * waveModifier);
                buzzerClone.GetComponent<EnemyController>().attackSpeed = (attackSpeed * 2f) + ((attackSpeed * 2f) * waveModifier);
                buzzerClone.GetComponent<EnemyController>().moveSpeed = moveSpeed - 10;
                buzzerClone.GetComponent<EnemyController>().resourceValue = resourceValue * 2;

                // Add to the currentEnemies array.
                currentEnemies[buzzerCount + spawnNum] = buzzerClone;

                // Increase enemy count for tracking purposes.
                buzzerCount++;
            }
        }
    }

    IEnumerator TankSpawner(int spawnNum)
    {
        while (tankCount < spawnNum)
        {
            yield return new WaitForSeconds(spawnTimer + 1.0f);
            int index = Random.Range(0, spawnPoints.Length);
            GameObject tankClone;

            // If it is a boss wave, spawn a larger more powerful version.
            if ((bossWave == true) && (tankCount == spawnNum - 1))
            {
                // Instantiate the enemy of the appropriate type.
                tankClone = (GameObject)Instantiate(tank, (spawnPoints[index].position), spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                tankClone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                tankClone.GetComponent<EnemyController>().healthPoints = (healthPoints * 3f) * 5;
                tankClone.GetComponent<EnemyController>().attackPower = (attackPower * 3f) * 2;
                tankClone.GetComponent<EnemyController>().attackSpeed = (attackSpeed * .5f) * 2;
                tankClone.GetComponent<EnemyController>().moveSpeed = moveSpeed - 25;
                tankClone.GetComponent<EnemyController>().resourceValue = (resourceValue * 4) * 10;

                tankClone.transform.localScale += new Vector3(1, 1, 1);

                // Add to the currentEnemies array.
                currentEnemies[tankCount + (spawnNum * 2)] = tankClone;

                // Increase enemy count for tracking purposes.
                tankCount++;
            }
            else
            {
                // Instantiate the enemy of the appropriate type.
                tankClone = (GameObject)Instantiate(tank, spawnPoints[index].position, spawnPoints[index].rotation);

                // Assign target to enemy according to which spawnpoint they spawn from.
                tankClone.GetComponent<EnemyController>().target = targets[index];

                // Assign enemy stats
                tankClone.GetComponent<EnemyController>().healthPoints = (healthPoints * 3f) + ((healthPoints * 3f) * waveModifier);
                tankClone.GetComponent<EnemyController>().attackPower = (attackPower * 3f) + ((attackPower * 3f) * waveModifier);
                tankClone.GetComponent<EnemyController>().attackSpeed = (attackSpeed * .5f) + ((attackSpeed * .5f) * waveModifier);
                tankClone.GetComponent<EnemyController>().moveSpeed = moveSpeed - 25;
                tankClone.GetComponent<EnemyController>().resourceValue = resourceValue * 4;

                // Add to the currentEnemies array.
                currentEnemies[tankCount + (spawnNum * 2)] = tankClone;

                // Increase enemy count for tracking purposes.
                tankCount++;
            }
        }
    }

    IEnumerator MasterMindSpawner(int spawnNum)
    {
        yield return new WaitForSeconds(spawnTimer + 1.0f);
        int index = Random.Range(0, spawnPoints.Length);
        GameObject masterMindClone;

        // Instantiate the enemy of the appropriate type.
        masterMindClone = (GameObject)Instantiate(mastermind, spawnPoints[index].position, spawnPoints[index].rotation);
        
        // Assign target to enemy according to which spawnpoint they spawn from.
        masterMindClone.GetComponentInChildren<EnemyController>().target = targets[index];

    // Assign enemy stats
        masterMindClone.GetComponentInChildren<EnemyController>().healthPoints = 10000;
        masterMindClone.GetComponentInChildren<EnemyController>().attackPower = 100;
        masterMindClone.GetComponentInChildren<EnemyController>().attackSpeed = 1;
        masterMindClone.GetComponentInChildren<EnemyController>().moveSpeed = 10;
        masterMindClone.GetComponentInChildren<EnemyController>().resourceValue = 10000;

        // Add to the currentEnemies array.
        currentEnemies[mastermindCount] = masterMindClone;

        // Increase enemy count for tracking purposes.
        mastermindCount++;
    }
}
