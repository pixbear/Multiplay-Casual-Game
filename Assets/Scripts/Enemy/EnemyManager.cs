using PB.MANAGER;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyStatus EnemyStatus => enemyStatus;
    [SerializeField] EnemyStatus enemyStatus;

    [SerializeField] private float spawnInterval = 5f;
    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        int randomPosX = Random.Range(-10, 10);
        int randomPosZ = Random.Range(-10, 10);
        Vector3 spawnPosition = new Vector3(randomPosX, 0f, randomPosZ);
        Enemy enemy = PObjectPoolManager.Instance.Get<Enemy>("Enemy", spawnPosition);
        enemy.Set(EnemyStatus);
    }
}