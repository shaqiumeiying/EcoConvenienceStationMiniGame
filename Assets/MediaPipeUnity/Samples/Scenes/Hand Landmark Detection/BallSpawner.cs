//using UnityEngine;

//public class BallSpawner : MonoBehaviour
//{
//    [Header("Spawner Settings")]
//    public GameObject[] goodBallPrefabs;
//    public GameObject[] badBallPrefabs;

//    [Range(0f, 1f)]
//    public float goodRate = 0.5f;

//    public HandTrackerTest handTracker;

//    public float spawnInterval = 1.5f;
//    public float spawnHeight = 1.5f;
//    public float xRange = 1.2f;
//    public float zPosition = 0f;

//    private float timer;

//    void Update()
//    {
//        timer += Time.deltaTime;

//        if (timer >= spawnInterval)
//        {
//            SpawnBall();
//            timer = 0f;
//        }
//    }

//    void SpawnBall()
//    {
//        bool isGood = Random.value < goodRate;

//        GameObject prefabToSpawn;

//        if (isGood)
//        {
//            prefabToSpawn = goodBallPrefabs[Random.Range(0, goodBallPrefabs.Length)];
//        }
//        else
//        {
//            prefabToSpawn = badBallPrefabs[Random.Range(0, badBallPrefabs.Length)];
//        }

//        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnHeight, zPosition);
//        GameObject newBall = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

//        BallCatcher catcher = newBall.GetComponent<BallCatcher>();
//        if (catcher != null)
//        {
//            catcher.handTracker = handTracker;
//            catcher.fallSpeed = Random.Range(1.0f, 3.0f);
//            catcher.isGoodBall = isGood;
//        }
//    }
//}

using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Ball Prefabs")]
    public GameObject[] goodBallPrefabs;
    public GameObject[] badBallPrefabs;

    [Range(0f, 1f)]
    public float goodRate = 0.5f;

    [Header("Spawn Position")]
    public float spawnHeight = 1.5f;
    public float xRange = 1.2f;
    public float zPosition = 0f;

    [Header("Spawn Delay Ranges (Random Each Game)")]
    public Vector2 minDelayRange = new Vector2(0.3f, 0.8f);
    public Vector2 maxDelayRange = new Vector2(1.2f, 2.4f);

    [Header("Actual Spawn Delay (Generated Each Game)")]
    public float minSpawnDelay;
    public float maxSpawnDelay;

    [Header("Hand Tracking")]
    public HandTrackerTest handTracker;

    private float nextSpawnTime;

    void Start()
    {
        GenerateRandomSpawnDelays();
        SetNextSpawnTime();
    }

    void GenerateRandomSpawnDelays()
    {

        minSpawnDelay = Random.Range(minDelayRange.x, minDelayRange.y);
        maxSpawnDelay = Random.Range(maxDelayRange.x, maxDelayRange.y);

        if (minSpawnDelay > maxSpawnDelay)
        {
            float tmp = minSpawnDelay;
            minSpawnDelay = maxSpawnDelay;
            maxSpawnDelay = tmp;
        }

        Debug.Log($"[Spawner] minSpawn={minSpawnDelay:F2}, maxSpawn={maxSpawnDelay:F2}");
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
            catcher.handTracker = handTracker;
        }
    }
}
