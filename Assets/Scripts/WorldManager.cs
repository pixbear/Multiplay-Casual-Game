using UnityEngine;
using PB.SYSTEM;
public class WorldManager : PMonoSingleton<WorldManager>
{
    [SerializeField] private Transform ground;
    [SerializeField] private float spawnMargin = 2f;
    [SerializeField] private float spawnHeight = 5f;

    private readonly Vector3 DEFAULT_GROUND_SIZE = new Vector3(50f, 50f, 50f);

    private Vector2 GetCurGroundSize()
    {
        var x = DEFAULT_GROUND_SIZE.x * ground.localScale.x;
        var z = DEFAULT_GROUND_SIZE.z * ground.localScale.z;
        return new Vector2(x, z);
    }


    public Vector3 GetRandomPos()
    {
        float radius = GetCurGroundSize().x / 2f;
        radius -= spawnMargin;

        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomDistance = Mathf.Sqrt(Random.Range(0f, 1f)) * radius;

        float randomPosX = Mathf.Cos(randomAngle) * randomDistance;
        float randomPosZ = Mathf.Sin(randomAngle) * randomDistance;

        Vector3 spawnPos = ground.position;
        spawnPos.x += randomPosX;
        spawnPos.z += randomPosZ;
        spawnPos.y = spawnHeight;

        return spawnPos;
    }
}
