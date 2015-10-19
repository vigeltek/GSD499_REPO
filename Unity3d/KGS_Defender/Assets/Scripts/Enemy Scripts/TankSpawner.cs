using UnityEngine;
using System.Collections;

public class TankSpawner : MonoBehaviour, iEnemyFactory
{
    // Variables for enemy stats.
    private float healthPoints = 100;                       // How much damage the enemy can take.       
    private float attackPower = 5;                          // How much damage each attack does.
    private float attackSpeed = 1;                          // How quickly the enemy attacks.
    private float moveSpeed = 50;                           // How quickly the enemy moves on the NavMesh.
    private int resourceValue = 50;                         // How many resources each enemy is worth.
    private float waveModifier;                             // Modifier to increase enemy stats depending on Wave.

	// Function to create one enemy of type enemyType
	public void SpawnEnemy (GameObject enemyType, int wave, Transform startLoc, bool bossWave)
    {
        // Set the wave modifier.
        waveModifier = ((wave - 1) * .25f);

        // Empty GameObject to hold instantiatied enemy.
        GameObject clone;

        // Create an instance of enemy.
        clone = (GameObject)Instantiate(enemyType, startLoc.position, startLoc.rotation);

        // If it is a boss wave, spawn a larger more powerful version last.
        if(bossWave == true)
        {
            // Assign enemy stats.
            clone.GetComponent<EnemyController>().healthPoints *= 3;
            clone.GetComponent<EnemyController>().attackPower *= 3;
            clone.GetComponent<EnemyController>().attackSpeed *= .90f;
            clone.GetComponent<EnemyController>().resourceValue *= 5;

            // Pass health and value to Stats script.
            clone.GetComponent<Stats>().health = clone.GetComponent<EnemyController>().healthPoints;
            clone.GetComponent<Stats>().recValue *= 10;

            // Increase the size of the model.
            clone.transform.localScale += new Vector3(1, 1, 1);
        }
        else
        {
            // Assign enemy stats.
            clone.GetComponent<EnemyController>().healthPoints *= 3;
            clone.GetComponent<EnemyController>().attackPower *= 3;
            clone.GetComponent<EnemyController>().attackSpeed *= .90f;
            clone.GetComponent<EnemyController>().resourceValue *= 5;

            // Pass health and value to Stats script.
            clone.GetComponent<Stats>().health = clone.GetComponent<EnemyController>().healthPoints;
            clone.GetComponent<Stats>().recValue *= 10;
        }

    }
}
