using UnityEngine;
using PB.SYSTEM;
public class WorldManager : PMonoSingleton<WorldManager>
{
    [SerializeField] private Transform world;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform ground;
    [SerializeField] private float spawnMargin = 2f;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private Transform[] pillars;

    [SerializeField] private Vector2 pillarHeightRange;
    [SerializeField] private Vector2 pillarScaleRange;
    
    private readonly Vector3 DEFAULT_GROUND_SIZE = new Vector3(50f, 50f, 50f);

    private void Start()
    {
        RandomBatchPillars();
    }
    
    private Vector2 GetCurGroundSize()
    {
        var x = DEFAULT_GROUND_SIZE.x * ground.localScale.x;
        var z = DEFAULT_GROUND_SIZE.z * ground.localScale.z;
        return new Vector2(x, z);
    }

    public void SetWorldSize(float sizes = 1f)
    {
        world.localScale = new Vector3(sizes, world.localScale.y, sizes);
    }

    public Vector3 GetRandomGroundPos()
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

    [ContextMenu("Randomize Pillars")]
    public void RandomBatchPillars()
    {
        foreach (var pillar in pillars)
        {
            float randomScale = Random.Range(pillarScaleRange.x, pillarScaleRange.y);
            float randomHeight = Random.Range(pillarHeightRange.x, pillarHeightRange.y);
            pillar.localScale = new Vector3(randomScale, pillar.localScale.y, randomScale);
            pillar.position = new Vector3(pillar.position.x, randomHeight, pillar.position.z);
        }
    }

    [SerializeField] private float hexagonRadius = 100f;
    // 6개의 기둥을 육각형모양으로 배치하는 함수
    [ContextMenu("Arrange Pillars In Hexagon")]
    public void ArrangePillarsInHexagon()
    {
        if (pillars.Length < 6) return;

        for (int i = 0; i < 6; i++)
        {
            float angle = i * Mathf.PI / 3f; // 60도 간격
            float x = Mathf.Cos(angle) * hexagonRadius;
            float z = Mathf.Sin(angle) * hexagonRadius;
            float y = -500f;
            pillars[i].position = new Vector3(x, y, z);
        }
    }
}
