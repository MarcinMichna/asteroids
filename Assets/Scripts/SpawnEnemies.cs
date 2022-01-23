using System;
using UnityEngine;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SpawnEnemies : NetworkBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabTable;

    [SerializeField]
    private float spawnInterval = 1.0f;

    [SerializeField]
    public float enemySpeed = 1.0f;

    [SerializeField] private float difficultyFactor;

    public override void OnStartServer()
    {
        InvokeRepeating("SpawnEnemy", this.spawnInterval, this.spawnInterval);
    }

    private void Update()
    {
        enemySpeed += difficultyFactor * Time.deltaTime;
        print(enemySpeed);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-4.0f, 4.0f), this.transform.position.y);
        int index = Random.Range(0, enemyPrefabTable.Length);
        GameObject enemy = Instantiate(enemyPrefabTable[index], spawnPosition, Quaternion.identity) as GameObject;
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -this.enemySpeed);
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 10);
    }
}