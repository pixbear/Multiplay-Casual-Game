using PB.MANAGER;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private bool spawnEnemy = true;
    public EnemyStatus EnemyStatus => enemyStatus;
    [SerializeField] EnemyStatus enemyStatus;

    [SerializeField] private float spawnInterval = 5f;
    private float spawnTimer;

    [ContextMenu("Set Enemy Status to Default")]
    private void ResetEnemyStatus() => enemyStatus = new EnemyStatus();

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
            spawnTimer = Mathf.Max(0.1f, spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnEnemy == false) return;
        Vector3 spawnPosition = GameManager.Instance.GetRandomSpawnPosForEnemy();
        Enemy enemy = PObjectPoolManager.Instance.Get<Enemy>("Enemy", spawnPosition);
        enemy.Set(EnemyStatus);
    }
}