using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnEnemies : NetworkBehaviour
{
    [SerializeField]
    private float enemySpeed = 3.0f;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnInterval = 2.0f;

    public override void OnStartServer()
    {
        InvokeRepeating("SpawnEnemy", this.spawnInterval, this.spawnInterval);
    }

    // Update is called once per frame
    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4f, 4f), this.transform.position.y, Random.Range(2f, 3f));
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
        enemy.GetComponent<Rigidbody>().velocity = new Vector3(enemy.transform.position.x, 0, -enemySpeed);
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 5f);
    }
}
