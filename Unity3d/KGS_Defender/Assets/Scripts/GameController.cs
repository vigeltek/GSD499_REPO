using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    #region // ********** Base Camp Variables ********** //
    // Variables to hold the Ship information.
    public GameObject ship;
    public float shipHealth;
    public Text shipHP;
    public Image shipHPBar;
    private float shipPerc;
    private bool shipDestroyed = false;

    // Variables to hold the Shield information.
    public GameObject shield;
    public float shieldHealth;
    public Text shieldHP;
    public Image shieldHPBar;
    private float shieldPerc;
    private bool shieldDestroyed = false;
    #endregion

    #region // ********** Wave Generation Variables ********** //
    // Create the enemy spawn factory.
    iEnemyFactory enemySpawner;

    // Prefabs for the Enemy types.
    public GameObject spider;
    public GameObject buzzer;
    public GameObject tank;
    public GameObject mastermind;
    private string[] waveNames = { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };

    // Array of spawnpoints enemies can randomly appear at.
    public Transform[] spawnPoints;

    // Miscellaneous variables for wave control.
    private int currentWave;                                // Number of the current wave.
    private int numToSpawn;                                 // How many enemies to spawn?
    private int spiderCount;                                 // How many enemies are there currently?
    private int buzzerCount;
    private int tankCount;
    private int mastermindCount;
    private int enemiesRemaining;                           // How many enemies are remaining?
    private bool waveComplete;
    private bool mastermindSpawned;                        // Has the final boss spawned?
    #endregion

    public float cash;
    public float score;
    private string txt;
    public GameObject DisplayLevel;
    private Text txtLevel;

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
        ship.GetComponent<Stats>().health = shipHealth;
        shield.GetComponent<Stats>().health = shieldHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            shipPerc = ((ship.GetComponent<Stats>().health / shipHealth) * 100);
            shipHP.text = string.Format(" SHIP: {0:##}%", shipPerc);
            shipHPBar.fillAmount = shipPerc / 100;
        }
        if (shield != null)
        { 
            shieldPerc = ((shield.GetComponent<Stats>().health / shieldHealth) * 100);
            shieldHP.text = string.Format(" SHIELDS: {0:##}%", shieldPerc);
            shieldHPBar.fillAmount = shieldPerc / 100;
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null && waveComplete == true)
        {
            currentWave++;
            StartCoroutine(StartNewWave(currentWave));
        }

        if (ship == null || (ship.GetComponent<Stats>().health <= 0 && shipDestroyed == false))
        {
            shipDestroyed = true;
        }

        if (shield == null || (shield.GetComponent<Stats>().health <= 0 && shieldDestroyed == false))
        {
            shieldDestroyed = true;
        }

        if (shipDestroyed == true && shipDestroyed == true)
        {
            if (!UIGameOverMenu.isOn)
            {
                UIGameOverMenu.Show();
                Time.timeScale = 0.1f;
            }
        }

        if (currentWave == 10 && GameObject.FindGameObjectWithTag("Enemy") == null && mastermindSpawned == true)
        {
            // Player wins
            UIGameOverWin.Show();
        }
	}

    void Awake()
    {
        instance = this;
        // Screen.SetResolution(1074, 768, true);

        if (DisplayLevel != null)
        {
            txtLevel = DisplayLevel.GetComponent<Text>();
        }
        currentWave = 0;
        numToSpawn = 0;
        spiderCount = 0;
        buzzerCount = 0;
        tankCount = 0;
        mastermindCount = 0;
        waveComplete = true;
        mastermindSpawned = false;
        UpdateLevel();
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

    public static void RestartLevel()
    {
        
        //instance.StartNewWave(instance.currentWave);
    }

    private void UpdateLevel()
    {
        if (DisplayLevel != null)
        {
            txtLevel.text = string.Format("WAVE: {0:00}/10", currentWave);
        }
    }


    IEnumerator StartNewWave(int wave)
    {
        waveComplete = false;
        UpdateLevel();


        UIGameMessage.DisplayMessage("Commander! Wave incoming in 15 seconds!");

        yield return new WaitForSeconds(15f);


        UIGameMessage.DisplayMessage("Commander! The " + waveNames[wave - 1] + " wave is beginning.");
        if(wave > 1)
        {

            UIGameMessage.DisplayMessage("Enemies appear to be " + (20 * (wave - 1)) + "% stronger!");
        }


        numToSpawn = 12 * wave;
        spiderCount = 0;
        buzzerCount = 0;
        tankCount = 0;
        mastermindCount = 0;

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
        while (spiderCount < toSpawn)
        {
            if (bossWave == true && (spiderCount == toSpawn - 1))
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new SpiderSpawner();
                enemySpawner.SpawnEnemy(spider, currentWave, spawnPoints[Random.Range(0, 2)], true);              
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new SpiderSpawner();
                enemySpawner.SpawnEnemy(spider, currentWave, spawnPoints[Random.Range(0, 2)], false);              
            }
            spiderCount++;
        }
        waveComplete = true;
    }

    IEnumerator Buzzers(int toSpawn, bool bossWave)
    {
        while (buzzerCount < toSpawn)
        {
            if (bossWave == true && (buzzerCount == toSpawn - 1))
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new BuzzerSpawner();
                enemySpawner.SpawnEnemy(buzzer, currentWave, spawnPoints[Random.Range(0, 2)], true);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new BuzzerSpawner();
                enemySpawner.SpawnEnemy(buzzer, currentWave, spawnPoints[Random.Range(0, 2)], false);
            }
            buzzerCount++;
        }
        waveComplete = true;
    }

    IEnumerator Tanks(int toSpawn, bool bossWave)
    {
        while (tankCount < toSpawn)
        {
            if (bossWave == true && (tankCount == toSpawn - 1))
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new TankSpawner();
                enemySpawner.SpawnEnemy(tank, currentWave, spawnPoints[Random.Range(0, 2)], true);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3.0f));
                enemySpawner = new TankSpawner();
                enemySpawner.SpawnEnemy(tank, currentWave, spawnPoints[Random.Range(0, 2)], false);
            }
            tankCount++;
        }
        waveComplete = true;
    }

    IEnumerator Mastermind()
    {
        yield return new WaitForSeconds(30);
        enemySpawner = new MastermindSpawner();
        enemySpawner.SpawnEnemy(mastermind, currentWave, spawnPoints[Random.Range(0, 2)], false);
        mastermindCount++;
        mastermindSpawned = true;
    }

    public void AddValues(float recVal)
    {
        AddResource(recVal);
        AddScore(recVal*10);
    }
    public float GetScore()
    {
        return score;
    }

    //return cash value.
    public float GetCash()
    {
        return cash;
    }

    //request permission to spend cash.
    public bool SpendCash(int value)
    {
        if ((cash - value) >= 0)
        {
            cash -= value;
            return true;
        }
        return false;
    }

    public void AddResource(float rec)
    {
        cash = cash + rec;
    }

    public void AddScore(float rec)
    {
        score = score + rec;
    }
}
