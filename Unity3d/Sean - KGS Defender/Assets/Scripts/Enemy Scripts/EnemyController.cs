using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{




    private int enemyCount;


    // Use this for initialization
    void Start()
    {
        enemyCount = 0;
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        //int index = Random.Range(0, spawnPoints.Length);
        //GameObject clone = (GameObject)Instantiate(enemyType, spawnPoints[index].position, spawnPoints[index].rotation);
        //clone.GetComponent<MoveToTarget>().target = targets[index];
        //clone.GetComponent<NavMeshAgent>().speed = moveSpeed;

        //enemyCount++;
        //currentEnemies[enemyCount] = clone;
    }
}
