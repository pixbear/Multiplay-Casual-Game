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
        Vector3 spawnPosition1 = WorldManager.Instance.GetRandomPos(10);
        EnemyBase enemy = objectPoolManager.Get<EnemyBase>("Enemy_Cube", spawnPosition1);
        enemy.Set(EnemyStatus);

        Vector3 spawnPosition2 = WorldManager.Instance.GetRandomPos(1);
        EnemyBase enemy2 = objectPoolManager.Get<EnemyBase>("Enemy_Sphere", spawnPosition2);
        enemy2.Set(EnemyStatus);
    }
}