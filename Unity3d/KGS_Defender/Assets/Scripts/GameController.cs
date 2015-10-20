using UnityEngine;
using System.Collections;

public enum _GameState { Play, Pause, Over }

public class GameController : MonoBehaviour 
{
    // handle in game messages
    public delegate void GameMessageHandler(string msg);
    public static event GameMessageHandler onGameMessageE;

    // display messages
    public static void DisplayMessage(string msg) { if (onGameMessageE != null) onGameMessageE(msg); }

    // handle game over
    public delegate void GameOverHandler(bool win); //true if win
    public static event GameOverHandler onGameOverE;

    // track game state
    public static _GameState GetGameState() {
        return instance.gameState;
    }

    // how much you get for selling a tower
    public float sellTowerRefundRatio = 0.5f;
    private float timeStep = 0.015f;
    public bool loadAudioManager = false;

    public string nextScene = "";
    public string mainMenu = "";

    public _GameState gameState = _GameState.Play;

    // Variable for the enemy target destination.
    public Transform destination;

    public static GameController instance;

    #region // ********** Wave Generation Variables ********** //

    // Create the enemy spawn factory.
    iEnemyFactory enemySpawner;

    // Prefabs for the Enemy types.
    public GameObject spider;
    public GameObject buzzer;
    public GameObject tank;
    public GameObject mastermind;

    // Array of spawnpoints enemies can randomly appear at.
    public Transform[] spawnPoints;

    // Miscellaneous variables for wave control.
    public int currentWave;                                // Number of the current wave.
    public int numToSpawn;                                 // How many enemies to spawn?
    public int enemyCount;                                 // How many enemies are there currently?
    public int enemiesRemaining;                           // How many enemies are remaining?
    private bool spawnWave;                                // Is it time to spawn a wave?
    #endregion

    public static void LoadMainMenu() {
        if (instance.mainMenu != "")
            Load(instance.mainMenu);
    }

    public static void Load(string levelName)
    {
        //if(gameState==_GameState.Ended && instance.playerLife>0){
        //	ResourceManager.NewSceneNotification();
        //}
        Application.LoadLevel(levelName);
    }
    public static void LoadGame()
    {
        //if(gameState==_GameState.Ended && instance.playerLife>0){
        //	ResourceManager.NewSceneNotification();
        //}
        Application.LoadLevel(instance.nextScene);
    }

    // Use this for initialization
    void Start()
	{
        StartNewWave(currentWave);
    }
	
	// Update is called once per frame
	void Update () 
	{

	}

    void Awake()
    {
        instance = this;
        // Screen.SetResolution(1074, 768, true);

        currentWave = 1;
        numToSpawn = 0;
        enemyCount = 0;
        enemiesRemaining = 0;
        spawnWave = true;
    }

    // add enable and disable functions
    void OnEnable()
    {
    }
    void OnDisable()
    {
    }

    public static void PauseGame()
    {
        instance.gameState = _GameState.Pause;
        Time.timeScale = 0;
    }
    public static void ResumeGame()
    {
        instance.gameState = _GameState.Play;
        Time.timeScale = 1;
    }

    IEnumerator WaveDelay()
    {
        yield return new WaitForSeconds(2f);
        currentWave++;
        spawnWave = true;
    }

    private void StartNewWave(int wave)
    {
        spawnWave = false;
        numToSpawn = 12 * wave;
        enemyCount = 0;
        enemiesRemaining = 0;

        switch (wave)
        {
            case 1:
            case 2:
                StartCoroutine(Spiders(numToSpawn, false));
                break;

            case 3:
                StartCoroutine(Spiders(numToSpawn, true));
                break;

            case 4:
            case 5:
                StartCoroutine(Spiders(numToSpawn / 2, false));
                StartCoroutine(Buzzers(numToSpawn / 2, false));
                break;

            case 6:
                StartCoroutine(Spiders(numToSpawn / 2, true));
                StartCoroutine(Buzzers(numToSpawn / 2, true));
                break;

            case 7:
            case 8:
                StartCoroutine(Spiders(numToSpawn / 3, false));
                StartCoroutine(Buzzers(numToSpawn / 3, false));
                StartCoroutine(Tanks(numToSpawn / 3, false));
                break;

            case 9:
                StartCoroutine(Spiders(numToSpawn / 3, true));
                StartCoroutine(Buzzers(numToSpawn / 3, true));
                StartCoroutine(Tanks(numToSpawn / 3, true));
                break;

            case 10:
                StartCoroutine(Spiders(numToSpawn / 3, true));
                StartCoroutine(Buzzers(numToSpawn / 3, true));
                StartCoroutine(Tanks(numToSpawn / 3, true));
                StartCoroutine(Mastermind());
                break;
        }
    }


    IEnumerator Spiders(int toSpawn, bool bossWave)
    {
        while (enemyCount < toSpawn)
        {
            if (bossWave == true && (enemyCount == toSpawn - 1))
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new SpiderSpawner();
                enemySpawner.SpawnEnemy(spider, currentWave, spawnPoints[Random.Range(0, 2)], true);
                enemyCount++;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new SpiderSpawner();
                enemySpawner.SpawnEnemy(spider, currentWave, spawnPoints[Random.Range(0, 2)], false);
                enemyCount++;
            }
        }
    }

    IEnumerator Buzzers(int toSpawn, bool bossWave)
    {
        while (enemyCount < toSpawn)
        {
            if (bossWave == true && (enemyCount == toSpawn - 1))
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new BuzzerSpawner();
                enemySpawner.SpawnEnemy(buzzer, currentWave, spawnPoints[Random.Range(0, 2)], true);
                enemyCount++;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new BuzzerSpawner();
                enemySpawner.SpawnEnemy(buzzer, currentWave, spawnPoints[Random.Range(0, 2)], false);
                enemyCount++;
            }
        }
    }

    IEnumerator Tanks(int toSpawn, bool bossWave)
    {
        while (enemyCount < toSpawn)
        {
            if (bossWave == true && (enemyCount == toSpawn - 1))
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new TankSpawner();
                enemySpawner.SpawnEnemy(tank, currentWave, spawnPoints[Random.Range(0, 2)], true);
                enemyCount++;
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new TankSpawner();
                enemySpawner.SpawnEnemy(tank, currentWave, spawnPoints[Random.Range(0, 2)], false);
                enemyCount++;
            }
        }
    }

    IEnumerator Mastermind()
    {
        yield return new WaitForSeconds(30);
        enemySpawner = new MastermindSpawner();
        enemySpawner.SpawnEnemy(mastermind, currentWave, spawnPoints[Random.Range(0, 2)], false);
        enemyCount++;
    }

    public void RemoveEnemy(float recVal)
    {
        GameObject GM = GameObject.FindGameObjectWithTag("GameManager");
        GM.GetComponent<GameManager>().AddResource(recVal);
        GM.GetComponent<GameManager>().AddScore(recVal);
        enemiesRemaining--;
    }
}
