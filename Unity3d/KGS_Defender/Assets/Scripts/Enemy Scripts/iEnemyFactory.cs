using UnityEngine;
using System.Collections;

public interface iEnemyFactory
{
    // Use this for initialization
    void SpawnEnemy(GameObject enemyType, int wave, Transform startLoc, bool bossWave);
}
