using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Ball Prefabs")]
    public GameObject[] goodBallPrefabs;
    public GameObject[] badBallPrefabs;

    [Range(0f, 1f)]
    public float goodRate = 0.5f;

    [Header("Spawn Settings")]
    public float spawnHeight = 1.5f;
    public float xRange = 1.2f;
    public float zPosition = 0f;

    public float minSpawnDelay = 0.3f;
    public float maxSpawnDelay = 2.0f;

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBall();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    void SpawnBall()
    {
        bool isGood = Random.value < goodRate;

        GameObject prefabToSpawn = isGood
            ? goodBallPrefabs[Random.Range(0, goodBallPrefabs.Length)]
            : badBallPrefabs[Random.Range(0, badBallPrefabs.Length)];

        Vector3 spawnPos = new Vector3(
            Random.Range(-xRange, xRange),
            spawnHeight,
            zPosition);

        GameObject newBall = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        BallCatcher catcher = newBall.GetComponent<BallCatcher>();
        if (catcher != null)
        {
            catcher.fallSpeed = Random.Range(1.0f, 3.0f);
            catcher.isGoodBall = isGood;
        }
    }
}
