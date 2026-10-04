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
        float radius = ground.localScale.x / 2f;
        radius -= spawnMargin;

        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomDistance = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        float randomPosX = Mathf.Cos(randomAngle) * randomDistance;
        float randomPosZ = Mathf.Sin(randomAngle) * randomDistance;

        Vector3 spawnPos = ground.position;
        spawnPos.x += randomPosX;
        spawnPos.z += randomPosZ;

        return spawnPos;
    }
}