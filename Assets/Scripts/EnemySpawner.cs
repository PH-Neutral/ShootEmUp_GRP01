using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool spawnBossNext = false;

    [SerializeField] Transform enemyPool = null;
    [SerializeField] Boss prefabBoss = null;
    [SerializeField] EnemyShip[] prefabEnemies = null;
    [SerializeField] float[] weightEnemies = null;
    [SerializeField] float spawnRate = 0.8f;
    bool canSpawn = false;
    float totalWeight, spawnTimer;
    int prefabIndex, nbEnemiesToSpawn, nbSpawnedEnemies;

    private void Awake() {
        float[] newWeights = new float[prefabEnemies.Length];
        totalWeight = 0;
        for(int i = 0; i < prefabEnemies.Length; i++) {
            newWeights[i] = i < weightEnemies.Length ? weightEnemies[i] : 1;
            totalWeight += newWeights[i];
        }
        weightEnemies = newWeights;
    }

    private void Start() {
        ChooseNewEnemy();
    }

    private void Update() {
        if(!canSpawn) return;
        if (spawnBossNext) {
            if (enemyPool.childCount < 1) {
                SpawnBoss();
            }
            return;
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) {
            //Debug.Log("Time's up!");
            if (SpawnEnemy()) {
                spawnTimer = 1f / spawnRate;
            }
        }
    }

    bool SpawnEnemy() {
        if(prefabEnemies == null || prefabEnemies.Length == 0) return false;
        if (nbSpawnedEnemies >= nbEnemiesToSpawn) {
            ChooseNewEnemy();
            return false;
        }
        Instantiate(prefabEnemies[prefabIndex], transform.position, Quaternion.identity, enemyPool);
        nbSpawnedEnemies++;
        return true;
    }

    void ChooseNewEnemy() {
        if(prefabEnemies.Length == 0) return;
        if(prefabEnemies.Length == 1) {
            prefabIndex = 0;
            return;
        }
        float rand = Random.Range(0f, totalWeight);
        float weightSum = 0;
        for(int i = 0; i < weightEnemies.Length; i++) {
            weightSum += weightEnemies[i];
            //Debug.Log($"TotalWeight: {totalWeight}; WeightSum: {weightSum}; rand: {rand}");
            if(rand <= weightSum) {
                prefabIndex = i;
                break;
            }
        }
        nbSpawnedEnemies = 0;
        nbEnemiesToSpawn = Random.Range(2,6);
        spawnTimer = 2.5f;
        canSpawn = true;
    }

    public void SpawnBoss() {
        Instantiate(prefabBoss, transform.position, Quaternion.identity, enemyPool);
        canSpawn = false;
    }
}
