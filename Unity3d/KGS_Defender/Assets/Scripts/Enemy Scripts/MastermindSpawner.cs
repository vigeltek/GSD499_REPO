using UnityEngine;
using System.Collections;

public class MastermindSpawner : iEnemyFactory
{
    // Variables for enemy stats.
    private float healthPoints = 2500;                              // How much damage the enemy can take.       
    private float attackPower = 1;                                  // How much damage each attack does.
    private float attackSpeed = .10f;                               // How quickly the enemy attacks.
    private float moveSpeed = 20;                                   // How quickly the enemy moves on the NavMesh.
    private float resourceValue = 10000;                             // How many resources each enemy is worth.
    private float waveModifier;                                     // Modifier to increase enemy stats depending on Wave.
    private float bossModifier;                                     // Modifier to increase boss enemy stats.

    // Function to create one enemy of type enemyType
    public void SpawnEnemy(GameObject enemyType, int wave, Transform startLoc, bool bossWave)
    {
        // Empty GameObject to hold instantiatied enemy.
        GameObject clone;

        // Create an instance of enemy.
        clone = (GameObject)GameObject.Instantiate(enemyType, startLoc.position, startLoc.rotation);

        // Assign enemy stats.
        clone.GetComponent<EnemyController>().healthPoints = healthPoints;
        clone.GetComponent<EnemyController>().attackPower = attackPower;
        clone.GetComponent<EnemyController>().attackSpeed = attackSpeed;
        clone.GetComponent<EnemyController>().moveSpeed = moveSpeed;
        clone.GetComponent<EnemyController>().resourceValue = resourceValue;

        // Pass health and value to Stats script.
        clone.GetComponent<Stats>().health = clone.GetComponent<EnemyController>().healthPoints;
        clone.GetComponent<Stats>().recValue = clone.GetComponent<EnemyController>().resourceValue;
        clone.name = "Mastermind";

        UIGameMessage.DisplayMessage("Commander!  Mastermind has arrived!");
    }

}