using PB.SYSTEM;
using UnityEngine;

public class GameManager : PMonoSingleton<GameManager>
{
    [SerializeField] bool isTestMode;
    public bool IsTestMode => isTestMode;

    [SerializeField] private Transform ground;
    [SerializeField] private float spawnMargin = 2f;

    public Vector3 GetRandomPos()
    {
        Vector3 size = ground.localScale;

        float halfX = size.x / 2f - spawnMargin;
        float halfZ = size.z / 2f - spawnMargin;

        float randomPosX = Random.Range(-halfX, halfX);
        float randomPosZ = Random.Range(-halfZ, halfZ);

        Vector3 spawnPos = ground.position;
        spawnPos.x += randomPosX;
        spawnPos.z += randomPosZ;

        return spawnPos;
    }
}
