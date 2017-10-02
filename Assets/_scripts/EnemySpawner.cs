using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    static public List<GameObject> activeEnemies; 

    [SerializeField]
    private float startWave = 3.0f;
    [SerializeField]
    private float exponent = 1.2f;

    [SerializeField]
    private float randomSpawnRangeX;
    [SerializeField]
    private float randomSpawnRangeY;

    private void Start()
    {
        activeEnemies = new List<GameObject>();
    }

    public void SpawnWave()
    {
        // spawn kick enemies
        for(int i = 0; i < Mathf.RoundToInt(startWave / 3); i++)
        {
            // Limit the kick to only spawn in the inner 60% of the normal spawn radius
            GameObject kEnemy = (GameObject)Instantiate(enemyPrefab, new Vector3(Random.Range(-randomSpawnRangeX * 0.6f, randomSpawnRangeX) * 0.6f, Random.Range(-randomSpawnRangeY * 0.6f, randomSpawnRangeY * 0.6f), 0), Quaternion.identity);
            kEnemy.GetComponent<EnemyComponent>().SetEnemyType(GameManager.HealthCategory.Kick);
            activeEnemies.Add(kEnemy);
        }

        for (int i = 0; i < Mathf.RoundToInt((2*startWave)/ 3); i++)
        {
            GameObject bEnemy = (GameObject)Instantiate(enemyPrefab, new Vector3(Random.Range(-randomSpawnRangeX, randomSpawnRangeX), Random.Range(-randomSpawnRangeY, randomSpawnRangeY), 0), Quaternion.identity);
            bEnemy.GetComponent<EnemyComponent>().SetEnemyType(GameManager.HealthCategory.Bass);
            activeEnemies.Add(bEnemy);

            GameObject hEnemy = (GameObject)Instantiate(enemyPrefab, new Vector3(Random.Range(-randomSpawnRangeX, randomSpawnRangeX), Random.Range(-randomSpawnRangeY, randomSpawnRangeY), 0), Quaternion.identity);
            hEnemy.GetComponent<EnemyComponent>().SetEnemyType(GameManager.HealthCategory.Hats);
            activeEnemies.Add(hEnemy);
        }

        startWave = Mathf.Pow(startWave, exponent);
        Debug.Log(startWave);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}