using UnityEngine;
using Unity.Netcode;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Singleton { get; private set; }
    [SerializeField] private Transform world;
    [SerializeField] private Transform wall;
    [SerializeField] private Transform ground;
    [SerializeField] private float spawnMargin = 2f;

    [SerializeField] private Transform pillarsContainer;
    [SerializeField] private Vector2 pillarHeightRange;
    [SerializeField] private Vector2 pillarScaleRange;
    
    private readonly Vector3 DEFAULT_GROUND_SIZE = new Vector3(50f, 50f, 50f);

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        RandomBatchPillars();
    }

    private Vector2 GetCurWorldSize()
    {
        var x = DEFAULT_GROUND_SIZE.x * world.localScale.x;
        var z = DEFAULT_GROUND_SIZE.z * world.localScale.z;
        return new Vector2(x, z);
    }

    public void SetWorldSize(float sizes = 1f)
    {
        world.localScale = new Vector3(sizes, world.localScale.y, sizes);
    }

    public Vector3 GetRandomPos(float spawnHeight = 5f)
    {
        float radius = GetCurWorldSize().x / 2f;
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

    [ContextMenu("Random Batch Pillars")]
    public void RandomBatchPillars()
    {
        foreach (Transform pillar in pillarsContainer)
        {
            float randomScale = Random.Range(pillarScaleRange.x, pillarScaleRange.y);
            float randomHeight = Random.Range(pillarHeightRange.x, pillarHeightRange.y);
            pillar.localScale = new Vector3(randomScale, pillar.localScale.y, randomScale);
            pillar.position = new Vector3(pillar.position.x, randomHeight, pillar.position.z);
        }
    }
}
