using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject ballPrefab;
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
        Vector3 spawnPos = new Vector3(Random.Range(-xRange, xRange), spawnHeight, 0);
        GameObject newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        BallCatcher catcher = newBall.GetComponent<BallCatcher>();
        if (catcher != null)
        {
            catcher.handTracker = handTracker;
            catcher.fallSpeed = Random.Range(1.0f, 3.0f);
        }
    }
}
