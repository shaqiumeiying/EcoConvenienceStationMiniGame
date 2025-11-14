//using UnityEngine;

//public class BallSpawner : MonoBehaviour
//{
//    [Header("Spawner Settings")]
//    public GameObject greenBallPrefab;  
//    public GameObject redBallPrefab;    
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

//        GameObject prefabToSpawn = (Random.value < 0.6f) ? greenBallPrefab : redBallPrefab;

//        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnHeight, zPosition);
//        GameObject newBall = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

//        BallCatcher catcher = newBall.GetComponent<BallCatcher>();
//        if (catcher != null)
//        {
//            catcher.handTracker = handTracker;
//            catcher.fallSpeed = Random.Range(1.0f, 3.0f);
//            catcher.isGoodBall = (prefabToSpawn == greenBallPrefab);
//        }
//    }
//}
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] goodBallPrefabs;  
    public GameObject[] badBallPrefabs;     

    [Range(0f, 1f)]
    public float goodRate = 0.5f;

    public HandTrackerTest handTracker;

    public float spawnInterval = 1.5f;
    public float spawnHeight = 1.5f;
    public float xRange = 1.2f;
    public float zPosition = 0f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBall();
            timer = 0f;
        }
    }

    void SpawnBall()
    {
        bool isGood = Random.value < goodRate;

        GameObject prefabToSpawn;

        if (isGood)
        {
            prefabToSpawn = goodBallPrefabs[Random.Range(0, goodBallPrefabs.Length)];
        }
        else
        {
            prefabToSpawn = badBallPrefabs[Random.Range(0, badBallPrefabs.Length)];
        }

        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnHeight, zPosition);
        GameObject newBall = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        BallCatcher catcher = newBall.GetComponent<BallCatcher>();
        if (catcher != null)
        {
            catcher.handTracker = handTracker;
            catcher.fallSpeed = Random.Range(1.0f, 3.0f);
            catcher.isGoodBall = isGood;
        }
    }
}
