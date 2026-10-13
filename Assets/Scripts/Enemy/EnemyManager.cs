using PB.MANAGER;
using Unity.Netcode;
using UnityEngine;

public class EnemyManager : NetworkBehaviour
{
    public static EnemyManager Singleton { get; private set; }

    [SerializeField] private bool spawnEnemy = true;
    [SerializeField] private float spawnInterval = 5f;

    public EnemyStatus EnemyStatus => enemyStatus;
    [SerializeField] EnemyStatus enemyStatus;

    private float spawnTimer;
    private PObjectPoolManager objectPoolManager;


    [ContextMenu("Set Enemy Status to Default")]
    private void ResetEnemyStatus() => enemyStatus = new EnemyStatus();

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        spawnTimer = spawnInterval;
        objectPoolManager = PObjectPoolManager.Singleton;
    }

    private void Update()
    {
        if (IsServer == false) return;
        
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
        Vector3 spawnPosition1 = WorldManager.Singleton.GetRandomPos(10);
        EnemyBase enemy = objectPoolManager.Get<EnemyBase>("Enemy_Cube", spawnPosition1);
        enemy.Set(EnemyStatus);

        Vector3 spawnPosition2 = WorldManager.Singleton.GetRandomPos(1);
        EnemyBase enemy2 = objectPoolManager.Get<EnemyBase>("Enemy_Sphere", spawnPosition2);
        enemy2.Set(EnemyStatus);
    }
}