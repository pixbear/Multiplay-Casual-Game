using PB.MANAGER;
using PB.SYSTEM;
using UnityEngine;

public class EnemyManager : PMonoSingleton<EnemyManager>
{
    [SerializeField] private bool spawnEnemy = true;
    [SerializeField] private float spawnInterval = 5f;

    public EnemyStatus EnemyStatus => enemyStatus;
    [SerializeField] EnemyStatus enemyStatus;

    private float spawnTimer;
    private PObjectPoolManager objectPoolManager;


    [ContextMenu("Set Enemy Status to Default")]
    private void ResetEnemyStatus() => enemyStatus = new EnemyStatus();

    private void Start()
    {
        spawnTimer = spawnInterval;
        objectPoolManager = PObjectPoolManager.Instance;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnEnemy();
            spawnTimer = Mathf.Max(0.1f, spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnEnemy == false) return;
        Vector3 spawnPosition = WorldManager.Instance.GetRandomPos(10);
        Enemy enemy = objectPoolManager.Get<Enemy>("Enemy", spawnPosition);
        enemy.Set(EnemyStatus);
    }
}