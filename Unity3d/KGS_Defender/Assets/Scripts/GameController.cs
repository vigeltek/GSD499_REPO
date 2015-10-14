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

    public GameObject[] enemyType;
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

    public static GameController instance;

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
        instance = this;
       // Screen.SetResolution(1074, 768, true);
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


}
